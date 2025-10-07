using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Configs.Env
{
    public class EnvService
    {
        public static (string model, string endpoint, string apiKey, string orgId) ReadFromEnvironment(AISource settingOption)
        {
            string _key = "";
            string _model = "";
            string _endpoint = "";
            string _orgId = "";

            if(settingOption == AISource.Azure)
            {
                _key = Environment.GetEnvironmentVariable("AZURE_OPEN_AI_APIKEY", EnvironmentVariableTarget.User);
                _model = Environment.GetEnvironmentVariable("AZURE_OPEN_AI_MODEL", EnvironmentVariableTarget.User);
                _endpoint = Environment.GetEnvironmentVariable("AZURE_OPEN_AI_ENDPOINT", EnvironmentVariableTarget.User);
                _orgId = Environment.GetEnvironmentVariable("AZURE_OPEN_AI_ORGID", EnvironmentVariableTarget.User);
                return (_model, _endpoint, _key, _orgId);
            }
            else if (settingOption == AISource.OpenAI)
            {
                _key = Environment.GetEnvironmentVariable("OPEN_AI_APIKEY", EnvironmentVariableTarget.User) ?? "";
                _model = Environment.GetEnvironmentVariable("OPEN_AI_MODEL", EnvironmentVariableTarget.User) ?? "";
                _endpoint = Environment.GetEnvironmentVariable("OPEN_AI_ENDPOINT", EnvironmentVariableTarget.User) ?? "";
                _orgId = Environment.GetEnvironmentVariable("OPEN_AI_ORGID", EnvironmentVariableTarget.User) ?? "";
            }
            else if (settingOption == AISource.Ollama)
            {
                _key = Environment.GetEnvironmentVariable("OLLAMA_AI_APIKEY", EnvironmentVariableTarget.User);
                _model = Environment.GetEnvironmentVariable("OLLAMA_AI_MODEL", EnvironmentVariableTarget.User);
                _endpoint = Environment.GetEnvironmentVariable("OLLAMA_AI_ENDPOINT", EnvironmentVariableTarget.User);
                _orgId = Environment.GetEnvironmentVariable("OLLAMA_AI_ORGID", EnvironmentVariableTarget.User);
            }

           return (_model, _endpoint, _key, _orgId);
        }
    }
}
