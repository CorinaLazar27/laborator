using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CreateStudent
{
    public static class QueueTrigger1
    {
        [Function("QueueTrigger1")]
        [TableOutput("student")]
        public static StudentEntity Run([QueueTrigger("coadalab6", Connection = "azurestoragecorina_STORAGE")] string myQueueItem,
            FunctionContext context)
        {

            var student = JsonConvert.DeserializeObject<StudentEntity>(myQueueItem);

            return student;
        }
    }
}
