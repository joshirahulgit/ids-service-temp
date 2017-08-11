using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class ResourceDuration : EntityBase
    {
        /// <summary>
        /// The time will override any other time duration inferred or calculated for the resource using a service with this attribute populated
        /// </summary>
        public int ActualDuration { get; set; }

        /// <summary>
        /// This time is added to the total time for the service
        /// </summary>
        public int SedationTime { get; set; }

        /// <summary>
        /// This time is added to the total time for the service 
        /// It represents the time BEFORE the actual appointment / resource engagement that the participants must be on location e.g. patient, tech etc.
        /// </summary>
        public int AdditionalLeadTime { get; set; }

        /// <summary>
        /// This time is added to the total time for the service - it can be supplied by some business rule or other process
        /// </summary>
        public int IncrementTime { get; set; }

        /// <summary>
        /// This time is deducted from the total time for the service - it can be supplied by some business rule or other process
        /// </summary>
        public int DecrementTime { get; set; }


        public ResourceDuration()
        {
        }

        public ResourceDuration(int actualDuration, int sedationTime, int additionalLeadTime, int incrementTime, int decrementTime)
        {
            ActualDuration = actualDuration;
            SedationTime = sedationTime;
            AdditionalLeadTime = additionalLeadTime;
            IncrementTime = incrementTime;
            DecrementTime = decrementTime;
        }

        //public static ResourceDurationDto Convert2Dto(ResourceDuration rd)
        //{
        //    var dto = new ResourceDurationDto
        //    {
        //        Id = rd.Id,
        //        ActualDuration = rd.ActualDuration,
        //        AdditionalLeadTime = rd.AdditionalLeadTime,
        //        DecrementTime = rd.DecrementTime,
        //        IncrementTime = rd.IncrementTime,
        //        SedationTime = rd.SedationTime
        //    };
        //    return dto;
        //}
    }
}
