using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DynabicBilling.Classes;
using DynabicBilling.RestApiDataContract;
using DynabicPlatform.Exceptions;
using WebApp.Classes;
namespace WebApp.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public PageMessageModel PageMessage { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public CreditCardModel CreditCard { get; set; }
        public BillingAddressModel BillingAddress { get; set; }
        public MyPlansModel Plans { get; set; }
        public PageMessageModel PageMessage { get; set; }

        #region Constructors
        public RegisterModel()
        {
            CreditCard = new CreditCardModel();
            BillingAddress = new BillingAddressModel();
        }
        public RegisterModel(BillingGateway dynabicBillingGateway)
            : this()
        {
            //get all your products
            Plans = new MyPlansModel(dynabicBillingGateway);
        }
        #endregion
    }

    public class ChangePlanModel
    {
        public MyPlansModel Plans { get; set; }
        public ProductResponse CurrentPlan { get; set; }
        public PageMessageModel PageMessage { get; set; }

        #region Constructors
        public ChangePlanModel() { }
        public ChangePlanModel(BillingGateway dynabicBillingGateway, int currentPlanId)
        {
            //get the current plan that this user is subscribed to
            CurrentPlan = dynabicBillingGateway.Products.GetProductById(currentPlanId.ToString());
            //get all your products
            Plans = new MyPlansModel(dynabicBillingGateway);
        }
        #endregion
    }

    public class MyPlansModel
    {
        [Display(Name = "Select a Service")]
        public int SelectedPlan { get; set; }
        public List<SelectListItem> MyPlans { get; set; }

        #region Constructors
        public MyPlansModel() { }
        public MyPlansModel(BillingGateway dynabicBillingGateway)
        {
            //get all your products
            MyPlans = GetAllPlans(dynabicBillingGateway);
        }
        #endregion

        /// <summary>
        /// Get a list containing all Products within a Site defined in Billing app
        /// sorted by Family Name and Product Name.
        /// </summary>
        /// <param name="dynabicBillingGateway">an instance of Bylling Gateway</param>
        /// <returns>a list o SelectListItem objects</returns>
        public List<SelectListItem> GetAllPlans(BillingGateway dynabicBillingGateway)
        {
            //instantiate your list
            List<SelectListItem> allPlans = new List<SelectListItem>();

            //get all your Products(Plans) through Billing Gateway 
            //and order them by Family Name then by Plan Name
            var myPlans = dynabicBillingGateway.Products.GetProductsBySite(Config.MySiteSubdomain)
                .OrderBy(o => o.FamilyId)
                .ThenBy(o => o.Name);

            //add the plans to the list
            if (myPlans != null)
            {
                foreach (var plan in myPlans)
                {
                    allPlans.Add(new SelectListItem() { Text = "(" + plan.FamilyId + ") " + plan.Name, Value = plan.Id.ToString() });
                }
            }
            return allPlans;
        }
    }

    public class BillingAddressModel
    {
        //Billing Address properties
        public int BillingAddressId { get; set; }

        [Required]
        [Display(Name = "Billing Address")]
        public string BillingAddress1 { get; set; }

        [Required]
        [Display(Name = "Billing City")]
        public string BillingCity { get; set; }

        [Required]
        [Display(Name = "Billing Zip/Postal Code")]
        public string BillingZipPostalCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string BillingCountry { get; set; }

        [Required]
        [Display(Name = "Province")]
        public string BillingProvince { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string BillingFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string BillingLastName { get; set; }

    }

    public class CreditCardModel
    {
        //Redit Card Properties
        public int Id { get; set; }

        [Required]
        [Display(Name = "CVV")]
        public string Cvv { get; set; }

        public DateTime? ExpirationDate
        {
            get { return new DateTime(this.SelectedYear, this.SelectedMonth, 1); }
            set
            {
                if (value.HasValue)
                {
                    this.SelectedMonth = value.Value.Month;
                    this.SelectedYear = value.Value.Year;
                }
                else
                {
                    this.SelectedMonth = DateTime.Now.Month;
                    this.SelectedYear = DateTime.Now.Year;
                }
            }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name on Card")]
        public string FirstNameOnCard { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Last Name on Card")]
        public string LastNameOnCard { get; set; }

        [Required]
        [Display(Name = "Credit Card Number")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Expiration Date")]
        public int SelectedMonth { get; set; }
        public IEnumerable<SelectListItem> Months
        {
            get
            {
                return DateTimeFormatInfo
                       .InvariantInfo
                       .MonthNames
                       .Where(month => !String.IsNullOrEmpty(month))
                       .Select((month, index) => new SelectListItem
                       {
                           Value = (index + 1).ToString(),
                           Text = (index + 1).ToString("00") + " - " + month
                       });
            }
        }

        [Required]
        public int SelectedYear { get; set; }
        public IEnumerable<SelectListItem> Years
        {
            get
            {
                //build an array of twelve years beginning with the currnt year
                int[] years = new int[12];
                for (int i = 0; i <= 11; i++)
                {
                    years[i] = (DateTime.Today.Year + i);
                }

                //return the years as a list of SelectListItem
                return years.Select((year) => new SelectListItem
                {
                    Value = year.ToString(),
                    Text = year.ToString()
                });
            }
        }

    }

    public class AccountInfoModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        public PageMessageModel PageMessage { get; set; }
    }

    public class PaymentInfoModel
    {
        public CreditCardModel CreditCard { get; set; }
        public BillingAddressModel BillingAddress { get; set; }
        public PageMessageModel PageMessage { get; set; }

        #region Constructors
        public PaymentInfoModel() { }
        public PaymentInfoModel(BillingGateway dynabicBillingGateway, string customerReferenceId)
            : this()
        {
            CreditCard = new CreditCardModel();
            BillingAddress = new BillingAddressModel();
            
            //get the first credit card of our customer to edit
            CreditCardResponse creditCard;
            try
            {
                creditCard = dynabicBillingGateway.Customer.GetFirstCreditCardForCustomerByReferenceId(Config.MySiteSubdomain, customerReferenceId);
            }
            catch (NotFoundException)
            {
                creditCard = new CreditCardResponse();
            }

            CreditCard.Id = creditCard.Id;
            CreditCard.FirstNameOnCard = creditCard.FirstNameOnCard;
            CreditCard.LastNameOnCard = creditCard.LastNameOnCard;
            CreditCard.ExpirationDate = creditCard.ExpirationDate;
            CreditCard.Number = creditCard.Number;
            CreditCard.Cvv = creditCard.Cvv;

            //get the first Billing Address of our customer to edit
            AddressResponse billingAddress;
            try
            {
                billingAddress = dynabicBillingGateway.Customer.GetFirstBillingAddressForCustomerByReferenceId(Config.MySiteSubdomain, customerReferenceId);
            }
            catch (NotFoundException)
            {
                billingAddress = new AddressResponse();
            }

            BillingAddress.BillingAddressId = billingAddress.Id;
            BillingAddress.BillingAddress1 = billingAddress.Address1;
            BillingAddress.BillingCity = billingAddress.City;
            BillingAddress.BillingCountry = billingAddress.Country;
            BillingAddress.BillingFirstName = billingAddress.FirstName;
            BillingAddress.BillingLastName = billingAddress.LastName;
            BillingAddress.BillingProvince = billingAddress.StateProvince;
            BillingAddress.BillingZipPostalCode = billingAddress.ZipPostalCode;
        }
        #endregion
    }

    public class SubscriptionDetailsModel
    {
        public SubscriptionDetailsModel()
        {
            this.PageMessage = new PageMessageModel();
            this.Transactions = new TransactionsList();
        }

        public PageMessageModel PageMessage { get; set; }

        [Display(Name = "Credit Card:")]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Current status:")]
        public SubscriptionStatus SubscriptionStatus { get; set; }

        [Display(Name = "Next assessment:")]
        public DateTime? NextAssesment { get; set; }

        [Display(Name = "Current Plan:")]
        public string CurrentPlanName { get; set; }

        [Display(Name = "Monthly Cost:")]
        public string MonthlyCost { get; set; }

        public TransactionsList Transactions { get; set; }

        public string CurrencyCode { get; set; }
    }
}
