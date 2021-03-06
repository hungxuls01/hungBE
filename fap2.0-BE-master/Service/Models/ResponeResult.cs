using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class ResponeResult
    {
        //status

        public ResponeResult(int status, string message, Object data, int id = 0)
        {
            Status = status;
            Message = message;
            Data = data;
            Id = id;
        }

        public int Id { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}