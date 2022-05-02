using System;
using System.Collections.Generic;
using System.Linq;

namespace JsModulesDemo.Error
{
    public class ErrorItemModel
    {
        public string Message { get; set; }
        public string TargetPath { get; set; }
    }

    public abstract class ErrorResultException : Exception
    {
        protected ErrorResultException(string message) : base(message)
        {
        }
    }

    public class ClientErrorResultException : ErrorResultException
    {
        private ClientErrorResultException(string message)
            : base(message)
        {
        }

        public ClientErrorResultException(string message, string targetPath)
            : base(message)
        {
            TargetPath = targetPath;
        }


        public static ErrorResultException Create(string message) => new ClientErrorResultException(message);
        public static ErrorResultException Create(string message, string targetPath) => new ClientErrorResultException(message, targetPath);

        public List<ErrorItemModel> Errors { get; set; } = new List<ErrorItemModel>();
        public string TargetPath { get; set; }
    }
}
