using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBookApp.Models
{
    public class SearchViewModel
    {
        [DisplayName("カナ")]
        [RegularExpression(@"[ァ-ヶ]+")] // 該当のプロパティに正規表現を追加する
        public string Kana { get; set; }

        public List<Address> Addresses { get; set; }
    }
}