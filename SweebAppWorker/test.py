import pandas as pd

tranco_list_path = 'tranco_4342X.csv'
list = pd.read_csv(tranco_list_path, header=None, names=["rank", "domain"])

def filter_urls(url):
    for domain in list['domain']:
        if(domain in url):
            return 'safe'
    else :
        return 'false'
    
print(filter_urls("https://dzen.ru"))
