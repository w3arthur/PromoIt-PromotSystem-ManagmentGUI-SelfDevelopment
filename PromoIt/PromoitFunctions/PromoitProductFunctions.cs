using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using PromotItLibrary.Models;
using PromotItLibrary.Classes;
using System.Threading;
using PromotItLibrary.Patterns;
using PromotItLibrary.Enums;
using PromotItLibrary.Interfaces.Users;
using PromotItLibrary.Classes.Users;

namespace PromoitFunction
{
    public static class PromoitProductFunctions
    {
        [FunctionName("PromoitProductFunctions")]
        public static async Task<IActionResult> Run(
                    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
                    ILogger log)
        {
            Modes FunctionOrDatabaseMode = Configuration.DatabaseMode;
            string azureFunctionString = "Azue Function";

            string className = "Data";
            log.LogInformation($"{azureFunctionString} Find {className} Activated");


            try
            {   //get

                string data = req.Query["data"];
                string type = req.Query["type"];
                if (data != null && type != null)
                {

                    try
                    {

                        if (type == "GetProductList")
                        {
                            className = "Get Product List";
                            ProductInCampaign productInCampaign = HTTPClient.JsonStringToSingleObject<ProductInCampaign>(data);
                            if (productInCampaign == null) throw new Exception($"GET: No {className} Found In Databae!");
                            List<ProductInCampaign> productInCampaignList = await productInCampaign.GetProductList_ListAsync(FunctionOrDatabaseMode);
                            log.LogInformation($"{azureFunctionString} Found {className}");
                            return new OkObjectResult(HTTPClient.ObjectToJsonString(productInCampaignList));
                        }

                        else if (type == "GetDonatedProductForShipping")
                        {
                            className = "Get Product List";
                            ProductDonated productDonated = HTTPClient.JsonStringToSingleObject<ProductDonated>(data);
                            if (productDonated == null) throw new Exception($"GET: No {className} Found In Databae!");
                            List<ProductDonated> productInCampaignList = await productDonated.GetDonatedProductForShipping_ListAsync(FunctionOrDatabaseMode);
                            log.LogInformation($"{azureFunctionString} Found {className}");
                            return new OkObjectResult(HTTPClient.ObjectToJsonString(productInCampaignList));
                        }

                        else if (type == "GetCashAmount")
                        {
                            className = "Activist Get Cash Amount";
                            ActivistUser activistUser = HTTPClient.JsonStringToSingleObject<ActivistUser>(data);
                            if (activistUser == null) throw new Exception($"GET: No {className} Found In Databae!");
                            IActivistUser activistUserData = await activistUser.GetCashAmountAsync(FunctionOrDatabaseMode);
                            log.LogInformation($"{azureFunctionString} Found {className}");
                            return new OkObjectResult(HTTPClient.ObjectToJsonString(activistUserData));
                        }

                    }
                    catch (Exception ex) { log.LogInformation($"{azureFunctionString} GET ({className}) Datanase SELECT/GET-data Fail:\n{ex.Message}"); return new BadRequestObjectResult($"Not Found ({className})"); }
                }
            }
            catch (Exception ex) { log.LogInformation($"{azureFunctionString} GET ({className}) Error Fail\n{ex.Message}"); }

            try
            {   //post
                using StreamReader streamReader = new StreamReader(req.Body);
                string requestBody = await streamReader.ReadToEndAsync();
                if (!string.IsNullOrEmpty(requestBody))
                {
                    requestBody = HTTPClient.HttpUrlDecode(requestBody);
                    Dictionary<string, string> keyValuePairs = HTTPClient.PostMessageSplit(requestBody);
                    string data = keyValuePairs["data"].ToString();
                    string type = keyValuePairs["type"].ToString();
                    try
                    {
                        bool action = false;

                        switch (type)
                        {

                            case "SetNewProduct":
                                className = "Set New Product";
                                ProductInCampaign productInCampaign = HTTPClient.JsonStringToSingleObject<ProductInCampaign>(data);
                                if (productInCampaign == null) throw new Exception($"POST: No {className} IS Enterd");
                                action = await productInCampaign.SetNewProductAsync(FunctionOrDatabaseMode);
                                break;

                            case "SetBuyAnItem":
                                className = "Buy An Item";
                                ProductDonated productDonated = HTTPClient.JsonStringToSingleObject<ProductDonated>(data);
                                if (productDonated == null) throw new Exception($"POST: No {className} IS Enterd");
                                action = await productDonated.SetBuyAnItemAsync(FunctionOrDatabaseMode);
                                break;


                            case "SetProductShipping":
                                className = "Set Product Shipping";
                                ProductDonated productDonated2 = HTTPClient.JsonStringToSingleObject<ProductDonated>(data);
                                if (productDonated2 == null) throw new Exception($"POST: No {className} IS Enterd");
                                action = await productDonated2.SetProductShippingAsync(FunctionOrDatabaseMode);
                                break;


                            default:
                                break;
                        }

                        if (action)
                        {
                            log.LogInformation($"{azureFunctionString} Seccess to Insert {className} to database");
                            return new OkObjectResult("ok");        //good result
                        }

                    }
                    catch (Exception ex) { log.LogInformation($"{azureFunctionString} Not-Seccess to Insert {className} to database\nDetails:{ex}"); return new BadRequestObjectResult("fail"); } //bad result
                    log.LogInformation($"{azureFunctionString} Failed to Insert after Tried to Insert {className} to database");

                    return new BadRequestObjectResult("(3) No access to type/database for " + type);
                }
            }
            catch (Exception ex) { log.LogInformation($"{azureFunctionString} POST ({className}) Error Fail:{ex.Message}"); return new BadRequestObjectResult($"Function Error Fail:{ex.Message}"); }


            return new BadRequestObjectResult("");//No Results
        }
    } 
}
