using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public static class OpenAIChatHelper
{
    public static string ClassifyDocument(string document)
    {
        string requestPrompt = "You will need to classify the documents into tags. ";
        requestPrompt += "Possible tags are: 'invoice', 'receipt', 'contract', 'quotation', 'agreement', 'other'. ";
        requestPrompt += "The tags are keywords that summarize the document's content. ";
        requestPrompt += "For each tag provided, you must specify the probability that the document belongs to that tag. ";
        requestPrompt += "The tags will be provided on a list. ";
        requestPrompt += "You must limit yourself to the supplied tags. ";
        requestPrompt += "You must not add other tags that are not in the list. ";
        requestPrompt += "All of the tags must be included when you reply. ";
        requestPrompt += "Only return the tags and no additional text. ";
        requestPrompt += "The following is a sample of the output you must return. You must return the same list, but replace the symbol 'RANKING' with the associated probability: ";
        requestPrompt += "invoice:RANKING, receipt:RANKING, contract:RANKING, quotation:RANKING, agreement:RANKING, other:RANKING ";
        requestPrompt += "The following is the document to analyze: ";
        requestPrompt += document;


        HttpClient client = new HttpClient
        {
            DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", Models.Constants.OPENAI_API_KEY)
                }
        };

        Request apiRequest = new Request
        {
            Messages = new[]
            {
                    new RequestMessage
                    {
                         Role = "system",
                         Content = "You are a helpful assistant in the classification of documents."
                    },
                    new RequestMessage
                    {
                         Role = "user",
                         Content = requestPrompt
                    }
                }
        };

        StringContent content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(apiRequest),
            Encoding.UTF8,
            "application/json"
        );

        HttpResponseMessage httpResponseMessage = client.PostAsync(
            "https://api.openai.com/v1/chat/completions",
            content
        ).Result;

        var test = httpResponseMessage.Content.ReadAsStringAsync().Result;

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            Response response = System.Text.Json.JsonSerializer.Deserialize<Response>(
                httpResponseMessage.Content.ReadAsStringAsync().Result
            );

            return response.Choices[0].Message.Content;
        }

        return null;
    }
}

