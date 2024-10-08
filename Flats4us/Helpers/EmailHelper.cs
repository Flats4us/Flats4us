﻿namespace Flats4us.Helpers
{
    public static class EmailHelper
    {
        public static string HtmlPTag(string value) =>
            $"<p>{value}</p>";

        public static string HtmlBTag(string value) =>
            $"<b>{value}</b>";

        public static string HtmlHTag(string value, byte hNumber) =>
            $"<h{hNumber}>{value}</h{hNumber}>";

        public static string AddLinkToText(string url, string linkText) =>
            $"<a href='{url}'>{linkText}</a>";

        public static string PrepareEmail(string value)
        {
            string backgroundColor = "#FBBB8B";

            string template = $@"
                <!DOCTYPE html>
                <html lang='pl'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                </head>
                <body style='background-color: {backgroundColor}; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>
                    <div style='background-color: {backgroundColor}; padding: 20px; font-size: 40px; color: white; text-align: left;'>
                        <b>Flats4us</b>
                    </div>
                    <div style='background-color: white; padding: 30px;'>
                        {value}
                    </div>
                    <div style='background-color: {backgroundColor}; padding: 20px; font-size: 15px; color: white; text-align: center;'>
                        Ta wiadomość została wygenerowana automatycznie, prosimy o nie odpowiadanie na nią
                    </div>
                </body>
                </html>";

            return template;
        }
    }
}
