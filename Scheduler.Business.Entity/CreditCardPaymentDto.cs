using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class CreditCardPaymentDto : EntityTypeDto
    {
        public string PayerName { get; set; }

        public AddressTypeDto BillingAddress { get; set; }

        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public string CardExpirationMonth { get; set; }

        public string CardExpirationYear { get; set; }

        public string CardCVV { get; set; }

        public CreditCardPaymentDto() { }

        public CreditCardPaymentDto(CreditCardPaymentDto creditCardPayment)
            : this()
        {
            this.PayerName = creditCardPayment.PayerName; ;
            this.BillingAddress = creditCardPayment.BillingAddress == null ? null : new AddressTypeDto(creditCardPayment.BillingAddress);
            this.CardType = creditCardPayment.CardType;
            this.CardNumber = creditCardPayment.CardNumber;
            this.CardExpirationMonth = creditCardPayment.CardExpirationMonth;
            this.CardExpirationYear = creditCardPayment.CardExpirationYear;
            this.CardCVV = creditCardPayment.CardCVV;
        }
    }

    public class ChequePaymentDto : EntityTypeDto
    {
        public string PayerName { get; set; }

        public string ChequeBankName { get; set; }

        public int ImagesCount { get; set; }

        public string ChequeRoutingNumber { get; set; }

        public string ChequeAccountNumber { get; set; }

        public string ChequeNumber { get; set; }

        public DateTime ChequeDate { get; set; }

        public List<ImageDto> ChequePictures { get; set; }

        public ChequePaymentDto()
        {
            ChequePictures = new List<ImageDto>();
        }

        public ChequePaymentDto(ChequePaymentDto chequePayment) : this()
        {
            this.PayerName = chequePayment.PayerName;
            this.ChequeBankName = chequePayment.ChequeBankName;
            this.ImagesCount = chequePayment.ImagesCount;
            this.ChequeRoutingNumber = chequePayment.ChequeRoutingNumber;
            this.ChequeAccountNumber = chequePayment.ChequeAccountNumber;
            this.ChequeNumber = chequePayment.ChequeNumber;
            this.ChequeDate = chequePayment.ChequeDate;

            this.ChequePictures = new List<ImageDto>();
            foreach (ImageDto chequePicture in chequePayment.ChequePictures)
            {
                this.ChequePictures.Add(new ImageDto(chequePicture));
            }
        }
    }
}
