﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class TechCompleteSuggestionListDto : DtoBase
    {
        public long Id { get; set; }

        public string DisplayName { get; set; }

        public bool IsVisible { get; set; }

        public bool IsDeleted { get; set; }
    }


    public class TechCompleteSuggestionsListDto : DtoBase
    {
        public TechCompleteSuggestionsListDto()
        {
            this.SuggestionList = new List<TechCompleteSuggestionListDto>();
        }

        public IList<TechCompleteSuggestionListDto> SuggestionList { get; set; }
    }
}
