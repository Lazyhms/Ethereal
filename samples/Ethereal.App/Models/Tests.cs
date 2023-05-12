using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ethereal.App.Models
{
    public class Tests
    {
        //[Requisite]
        public DateTime? Create { get; set; }

        [FileLength(1)]
        public IFormFile File { get; set; }

        [FileExtensions]
        public string FileExtensions { get; set; }

        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 控制
        /// </summary>
        [Required, IDCard]
        public string IDCard { get; set; }

        [Required]
        public IEnumerable<int> IEnumerableLength { get; set; }

        [SocialCreditCode]
        public string SocialCreditCode { get; set; }

        [ModelBinder()]
        public DateTimeOffset? Timestamp { get; set; }

        public string Name => "sss";
    }
}