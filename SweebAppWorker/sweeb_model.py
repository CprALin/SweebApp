from flask import Flask, request, jsonify
import torch
import pandas as pd
from transformers import AutoModelForSequenceClassification, AutoTokenizer,pipeline

app = Flask(__name__)

sweeb_model_path = "sweeb_model"

sweeb_model = AutoModelForSequenceClassification.from_pretrained(sweeb_model_path)
sweeb_tokenizer = AutoTokenizer.from_pretrained(sweeb_model_path)
sweeb_classifier = pipeline(
        "text-classification",
        model=sweeb_model,
        tokenizer=sweeb_tokenizer,
        device=0 if torch.cuda.is_available() else -1,
        return_all_scores=True
)


'''
    We use the Tranco list* [1] generated on 26 May 2026, ...
    * Available at https://tranco-list.eu/list/4342X.
'''
tranco_list_path = 'tranco_4342X.csv'

list = pd.read_csv(tranco_list_path, header=None, names=["rank", "domain"])

def filter_urls(url):
    for domain in list['domain']:
        if(domain in url):
            return {'label' : 'safe' , 'score' : 0.96}
    else :
        return 'false'
    

def predict_sweeb(url):

    label_mapping = {"LABEL_0" : "safe", "LABEL_1" : "threat"}
    result = sweeb_classifier(url)

    label = result[0]['label']
    score = result[0]['score']
    friendly_label = label_mapping.get(label,label)

    return { "label": friendly_label, "score": score }

@app.route("/")
def welcome():
    return "Welcome to SweebAppModel"

@app.route("/model_predict",methods=["POST"])
def useModel():
    data = request.get_json()
    url = data.get("url")

    response = filter_urls(url)
    if(response == 'false'):
        response = predict_sweeb(url)

    return jsonify({
        "url": url,
        "prediction": response
    })
