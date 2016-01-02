using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.ProductEditor.Models
{
    public class QAModel//: BaseNopModel//baseNopEntityModel ima še id zraven
    {
        public QAModel() {
            Answers = new List<Answer>();/*k okreiraš model se ustavi tale objekt, da ga potem lahko nastavljaš*/
        }

        public int Id { get; set; }
        public int ControlTypeId { get; set; }
        public String Question { get; set; }
        public List<Answer> Answers { get; set; }
    }

    public class Answer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsPreSelected { get; set; }
        //public Boolean IsSelected { get; set; }
    }

    public class QAProfile //: BaseNopModel//baseNopEntityModel ima še id zraven
    {
        public QAProfile()
        {
            ProfileAnswers = new List<ProfileAnswer>();/*k okreiraš model se ustavi tale objekt, da ga potem lahko nastavljaš*/
        }

        public int ProfileId { get; set; }
        public String Name { get; set; }
        public List<ProfileAnswer> ProfileAnswers { get; set; }
    }

    public class ProfileAnswer
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }

}
