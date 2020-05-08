using System;
using System.Linq;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;
using Google.Cloud.Dialogflow.V2;
using static System.Console;

// https://github.com/GoogleCloudPlatform/dotnet-docs-samples/tree/master/dialogflow/api/DialogflowSamples

namespace ConsoleAssistant
{
    // Samples demonstrating how to detect Intents using texts
    public class DetectIntentTexts
    {
        public static void RegisterCommands(VerbMap<object> verbMap)
        {
            verbMap
                .Add((DetectIntentFromTextsOptions opts) =>
                     DetectIntentFromTexts(opts.ProjectId, opts.SessionId, opts.Texts, opts.LanguageCode));
        }

        [Verb("detect-intent:texts", HelpText = "Detect Intent using provided texts")]
        public class DetectIntentFromTextsOptions : OptionsWithProjectIdAndSessionId
        {
            [Value(0, MetaName = "texts", HelpText = "Comma separated text input", Required = true)]
            public string TextsInput { get; set; }

            public string[] Texts => TextsInput.Split(',');

            [Value(1, MetaName = "languageCode", HelpText = "Language code, eg. en-US", Default = "en-US")]
            public string LanguageCode { get; set; }
        }

        // [START dialogflow_detect_intent_text]
        public static int DetectIntentFromTexts(string projectId,
                                                string sessionId,
                                                string[] texts,
                                                string languageCode = "en-US")
        {
            var client = SessionsClient.Create();

            foreach (var text in texts)
            {
                var response = client.DetectIntent(
                    session: SessionName.FromProjectSession(projectId, sessionId),
                    queryInput: new QueryInput()
                    {
                        Text = new TextInput()
                        {
                            Text = text,
                            LanguageCode = languageCode
                        }
                    }
                );

                var queryResult = response.QueryResult;

                Console.WriteLine($"Query text: {queryResult.QueryText}");
                if (queryResult.Intent != null)
                {
                    Console.WriteLine($"Intent detected: {queryResult.Intent.DisplayName}");
                }
                Console.WriteLine($"Intent confidence: {queryResult.IntentDetectionConfidence}");
                Console.WriteLine($"Fulfillment text: {queryResult.FulfillmentText}");
                Console.WriteLine();
            }

            return 0;
        }
        // [END dialogflow_detect_intent_text]
    }
}
