using System;
using System.Runtime.InteropServices;
using Microsoft.Maui;

namespace SweebAppFront.Platforms.Windows;

public static class WindowChromeHelper
{
    private const int GWL_STYLE = -16;

    private const long WS_CAPTION = 0x00C00000L;
    private const long WS_THICKFRAME = 0x00040000L;
    private const long WS_MINIMIZEBOX = 0x00020000L;
    private const long WS_MAXIMIZEBOX = 0x00010000L;
    private const long WS_SYSMENU = 0x00080000L;

    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOMOVE = 0x0002;
    private const uint SWP_NOZORDER = 0x0004;
    private const uint SWP_FRAMECHANGED = 0x0020;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int X,
        int Y,
        int cx,
        int cy,
        uint uFlags);

    public static void MakeFrameless(IntPtr hwnd)
    {
        var style = GetWindowLongPtr(hwnd, GWL_STYLE).ToInt64();

        style &= ~WS_CAPTION;      // remove bar
        style &= ~WS_THICKFRAME;   // remove border
        style &= ~WS_MINIMIZEBOX;  // remove minimize
        style &= ~WS_MAXIMIZEBOX;  // remove maximize
        style &= ~WS_SYSMENU;      // remove close

        SetWindowLongPtr(hwnd, GWL_STYLE, new IntPtr(style));

        SetWindowPos(
            hwnd,
            IntPtr.Zero,
            0, 0, 0, 0,
            SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
    }
}