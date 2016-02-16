using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    /// <summary>
    /// Simple client request model
    /// </summary>
    public class UnusualRequests
    {
        /// <summary>
        /// Unique id for this request
        /// </summary>
        [Key]
        public int RequestId { get; set; }
        /// <summary>
        /// Clients full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Clients question or request
        /// </summary>
        public string QuestionOrRequest { get; set; }

        /// <summary>
        /// The date and time the request occured
        /// </summary>
        public DateTime Occurrence { get; set; }
    }
}
