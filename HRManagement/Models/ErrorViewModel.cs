using System;

namespace HRManagement.Models
{
    /// <summary>
    /// Model for error view
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}