using Hoshin.CrossCutting.Authorization.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Application.UseCases.Claim
{
    public class GetAllClaimsUseCase : IGetAllClaimsUseCase
    {
        private const string PROJECT_QUALITY = "Quality";
        private const string PROJECT_CORE = "Core";
        public List<KeyValuePair<string,List<KeyValuePair<string,List<KeyValuePair<string, string>>>>>> Execute()
        {
            //    var allClaims = new List<KeyValuePair<string, List<KeyValuePair<string, string>>>>();
            var allClaims = new
                //key = project
                //value = modules
                List<KeyValuePair<string,
                    //key = module
                    //value = field
                    List<KeyValuePair<string,
                        //key = field
                        //value = claim
                        List<KeyValuePair<string, string>>>>>>();

            Type typeClaim = typeof(IClaim);


            //Get all types inherited from IClaim
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeClaim.IsAssignableFrom(p));

            //Initialize claims for every project
            var claimsQuality = new List<KeyValuePair<string, List<KeyValuePair<string, string>>>>();
            var claimsCore = new List<KeyValuePair<string, List<KeyValuePair<string, string>>>>();

            foreach (var type in types.ToList())
            {
                //I dont have interest in return the interface so skip this
                if (type.Name != "IClaim")
                {
                    var claimsValues = new List<KeyValuePair<string, string>>();
                    var fields = type.GetFields();
                    foreach (var field in fields)
                    {
                        //Add the name property and his value as a keyvaluepair in the list
                        claimsValues.Add(new KeyValuePair<string, string>(field.Name, field.GetValue(field.Name).ToString()));
                        Console.Write(field.Name);
                    }
                    switch (type.GetProperties().FirstOrDefault().Name)
                    {
                        case PROJECT_QUALITY:
                            //type.Name = Finding/Ac/ anthing inherited of Quality project.
                            //Add to list of claims of quality
                            claimsQuality.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>(type.Name, claimsValues));
                            break;
                        case PROJECT_CORE:
                            claimsCore.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>(type.Name, claimsValues));
                            break;
                        default:
                            break;
                    }
                }

            }

            //Add list of project claims to object to return
            allClaims.Add(new KeyValuePair<string, List<KeyValuePair<string, List<KeyValuePair<string, string>>>>>("Quality", claimsQuality));
            allClaims.Add(new KeyValuePair<string, List<KeyValuePair<string, List<KeyValuePair<string, string>>>>>("Core", claimsCore));

            return allClaims;
        }
    }
}
