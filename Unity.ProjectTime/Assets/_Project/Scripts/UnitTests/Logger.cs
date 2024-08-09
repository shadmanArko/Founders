using System;

public class Logger
{
    public Logger()
    {
        Log = "";
    }

    public string Log
    {
        get;
        private set;
    }

    public void Write(string value)
    {
        if (value == null)
        {
            throw new ArgumentException();
        }

        Log += value;
    }
}