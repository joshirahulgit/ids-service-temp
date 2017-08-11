using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class TechCompleteSuggestionList : EntityBase
    {
        public string DisplayName { get; private set; }
        public bool IsVisible { get; private set; }
        public bool IsDeleted { get; private set; }

        public TechCompleteSuggestionList(long id, string displayName, bool isVisible)
        {
            this.Id = id;
            DisplayName = displayName;
            IsVisible = isVisible;
        }

        //public static TechCompleteSuggestionList ExtractFromDto(TechCompleteSuggestionListDto dto)
        //{
        //    TechCompleteSuggestionList r = new TechCompleteSuggestionList(dto.Id, dto.DisplayName, dto.IsVisible)
        //    { IsDeleted = dto.IsDeleted };
        //    return r;
        //}

        //public static TechCompleteSuggestionListDto Convert2Dto(TechCompleteSuggestionList suggestionList)
        //{
        //    TechCompleteSuggestionListDto r = new TechCompleteSuggestionListDto();
        //    r.Id = suggestionList.Id;
        //    r.DisplayName = suggestionList.DisplayName;
        //    r.IsVisible = suggestionList.IsVisible;

        //    return r;
        //}
    }
}
