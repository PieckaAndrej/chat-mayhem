﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class Question
    {
        public int id { get; set; }

        public string text { get; set; }

        public List<Answer> answers { get; set; }

        public QuestionPack QuestionPack { get; set; }

        public Question(int id, string text)
        {
            this.id = id;
            this.text = text;
        }

        public Question(int id, string text, List<Answer> answers) : this(id, text)
        {
            this.answers = answers;
        }
    }
}