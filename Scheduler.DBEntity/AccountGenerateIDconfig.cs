using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AccountGenerateIDconfig : EntityBase
    {
        public string AccountId { get; set; }
        public string Location { get; set; }
        public string CustomLocationCode { get; set; }
        public string IdTypeName { get; set; }
        public string IDFormatString { get; set; }
        public string PreFix { get; set; }
        public string PostFix { get; set; }
        public int? StartingSeq { get; set; }
        public int? NextAvailableSeq { get; set; }
        public bool? IsSeqPadded { get; set; }
        public int? SeqPaddingLen { get; set; }
        public string SeqPaddingChar { get; set; }
        public string SeqPaddingDir { get; set; }
        public bool UseGuid { get; set; }
        public int? GuidLen { get; set; }

        public AccountGenerateIDconfig()
        {
        }

        public AccountGenerateIDconfig(string accountName, string location)
        {
            this.AccountId = accountName;
            this.Location = location;
        }

        public void SetDefaultConfiguration(string typeName)
        {
            //Sunil: this logic is based on the original GetOrderID.aspx page from AbbaDox and will be used
            // as a backup for unconfigured accounts
            this.UseGuid = true;
            this.GuidLen = 16;
            this.IdTypeName = typeName;
            this.IDFormatString = "[guid]";
            this.PostFix = "IDS";
        }

        //public string GetNewId(RepositoryLocator locator)
        //{
        //    string previousId = string.Empty, newId;
        //    do
        //    {
        //        if (!this.UseGuid)
        //        {
        //            /*
        //             * we need to reserve a sequence if sequencing is used instead of guid
        //             * for guid skip the reservation
        //            */
        //            NextAvailableSeq = locator.AccountRepository.ReserveNewSeqForId(this.Id);
        //        }
        //        newId = GenerateNewId();
        //        /*
        //         * need to loop until newId does not exist in the target account and id type
        //        */
        //        if (previousId == newId)
        //            throw new SchedulerException(SchedulerExceptionType.GuidGenerationSequenceFailed,
        //                string.Format("Account {0} has invalid Id generation setting. Sequence not changing. Value={1}", AccountId, newId));
        //        previousId = newId;
        //    } while (locator.AccountRepository.CheckIfNewIdExistsInAccount(newId, IdTypeName.ToUpper()));

        //    return newId;
        //}

        private string GenerateNewId()
        {
            StringBuilder result = new StringBuilder("");
            List<string> vars = new List<string>();
            List<string> seps = new List<string>();
            try
            {
                Regex regex = new Regex(@"(?<Vars>\[[a-z0-9-_]*\])(?<Seps>[-_]?)", RegexOptions.IgnoreCase);
                MatchCollection matColl = regex.Matches(this.IDFormatString.ToLower());
                foreach (Match m in matColl)
                {
                    string val = (m.Groups["Vars"].Success ? m.Groups["Vars"].Value : null);
                    if (!string.IsNullOrEmpty(val))
                    {
                        vars.Add(val);
                        val = (m.Groups["Vars"].Success ? m.Groups["Seps"].Value : null);
                        if (!string.IsNullOrEmpty(val)) seps.Add(val); else seps.Add("");
                    }
                }
                for (int i = 0; i < vars.Count; i++)
                {
                    string var = string.Empty;
                    switch (vars[i])
                    {
                        case "[pre]":
                            if (!string.IsNullOrEmpty(this.PreFix))
                                var = this.PreFix;
                            break;
                        case "[loc]":
                            if (!string.IsNullOrEmpty(this.CustomLocationCode))
                                var = this.CustomLocationCode;
                            else if (!string.IsNullOrEmpty(this.Location))
                                var = this.Location;
                            break;
                        case "[seq]":
                            var = this.NextAvailableSeq.ToString();
                            break;
                        case "[post]":
                            if (!string.IsNullOrEmpty(this.PostFix))
                                var = this.PostFix;
                            break;
                        case "[guid]":
                        default:
                            string guid = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                            if (this.GuidLen != null && this.GuidLen > 1 && this.GuidLen <= 32)
                                var = guid.Substring(0, (int)this.GuidLen);
                            else
                                var = guid.Substring(0, 16);
                            break;
                    }
                    if (!string.IsNullOrEmpty(var))
                    {
                        result.Append(var);
                        result.Append(seps[i]);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
            }

            return result.ToString();
        }
    }
}
