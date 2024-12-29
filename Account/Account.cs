using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Account
{
    public class Account : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));


            Entity entity = (Entity)context.InputParameters["Target"];
            string messageName = context.MessageName;

            if(messageName == "Create")
            {
            CheckName(entity);

            }
            CheckCredit(entity);
            //tracingService.Trace("Step 1");

            
        }


        public void CheckCredit(Entity entity)
        {
            //Credit limit is 500k
            decimal maxCredit = 500000;
            decimal currentCredit = 0;
            if (entity.Contains("creditlimit"))
            {
                currentCredit = ((Money)entity["creditlimit"]).Value;
            }

            if (currentCredit > maxCredit)
            {
                throw new InvalidPluginExecutionException("Credit is too high!");
            }
        }

        public void CheckName(Entity entity)
        {
            
            if (entity.Contains("name"))
            {
                //check to make uppercase
                entity["name"] = entity["name"].ToString().ToUpper();
            }
            
            
        }
    }
}
