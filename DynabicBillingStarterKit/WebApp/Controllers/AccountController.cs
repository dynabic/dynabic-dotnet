using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DynabicBilling.RestApiDataContract;
using DynabicPlatform.Exceptions;
using WebApp.Classes;
using WebApp.Models;

namespace WebApp.Controllers
{

    public class AccountController : BaseController
    {

        #region LogIn
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonLogin

        [AllowAnonymous]
        [HttpPost]
        public JsonResult JsonLogin(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return Json(new { success = true, redirect = returnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region LogOff
        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register
        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterModel model = new RegisterModel(_dynabicBillingGateway);
            model.CreditCard.ExpirationDate = DateTime.Now;
            model.PageMessage = TempData["PageMessage"] as PageMessageModel ?? new PageMessageModel();

            return View(model);
            // return ContextDependentView();
        }

        //
        // POST: /Account/JsonRegister

        [AllowAnonymous]
        [HttpPost]
        public ActionResult JsonRegister(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                //if the registration process was successful,
                //add the newly registered user to your Customers and subscribe him to the Selected Plan
                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    try
                    {
                        //get your Site data through Billing Gateway
                        var mySite = _dynabicBillingGateway.Sites.GetSiteBySubdomain(Config.MySiteSubdomain);

                        //get the selected plan
                        var selectedPlan = _dynabicBillingGateway.Products.GetProductById(model.Plans.SelectedPlan.ToString());

                        //generate a random Customer Reference Id
                        //CustomerReferenceId does not permit the usage of any other special characters except "-" and
                        //it isn't allowed to begin or end with this character nor it can be consecutive
                        Random r = new Random();
                        long value = (long)((r.NextDouble() * 2.0 - 1.0) * long.MaxValue);
                        string newCustomerReferenceId = string.Format("{0}-{1}", mySite.Id, Math.Abs(value));

                        //create a new CustomerRequest
                        CustomerRequest newCustomer = new CustomerRequest()
                        {
                            //this fields are required
                            FirstName = model.UserName,
                            LastName = model.UserName,
                            Email = model.Email,
                            ReferenceId = newCustomerReferenceId
                        };

                        //create a new Subscription Request
                        SubscriptionRequest newSubscription = new SubscriptionRequest()
                        {
                            Customer = newCustomer,
                            ProductId = selectedPlan.Id,
                            ProductPricingPlanId = selectedPlan.PricingPlans[0].Id,
                        };

                        //if the Credit Card is required at Signup 
                        //create a new Credit Card Request and add it to your Subscription
                        //isCreditCardAtSignupRequired may be "No", "Yes", "YesOptional"
                        if (selectedPlan.isCreditCardAtSignupRequired != BoolOptional.No)
                        {
                            CreditCardRequest newCreditCard = new CreditCardRequest()
                            {
                                Cvv = model.CreditCard.Cvv,
                                ExpirationDate = model.CreditCard.ExpirationDate,
                                FirstNameOnCard = model.CreditCard.FirstNameOnCard,
                                LastNameOnCard = model.CreditCard.LastNameOnCard,
                                Number = model.CreditCard.Number
                            };

                            newSubscription.CreditCard = newCreditCard;
                        }
                        //if the Billing Address is required at Signup 
                        //create a new Billing Address Request and add it to your Subscription
                        //isBillingAddressAtSignupRequired may be "No", "Yes", "YesOptional"
                        if (selectedPlan.isBillingAddressAtSignupRequired != BoolOptional.No)
                        {
                            AddressRequest billingAddress = new AddressRequest()
                            {
                                Address1 = model.BillingAddress.BillingAddress1,
                                City = model.BillingAddress.BillingCity,
                                Country = model.BillingAddress.BillingCountry,
                                StateProvince = model.BillingAddress.BillingProvince,
                                ZipPostalCode = model.BillingAddress.BillingZipPostalCode,
                                FirstName = model.BillingAddress.BillingFirstName,
                                LastName = model.BillingAddress.BillingLastName
                            };
                            newSubscription.BillingAddress = billingAddress;
                        }

                        //subscribe the newly created Customer to selected Plan
                        _dynabicBillingGateway.Subscription.AddSubscription(mySite.Subdomain, newSubscription);

                        //create a User Profile and save some data that we may need later
                        ProfileCommon profile = ProfileCommon.GetUserProfile(model.UserName);
                        profile.CurrentPlanId = model.Plans.SelectedPlan;
                        profile.CustomerReferenceId = newCustomerReferenceId;

                        //redirect to Home page 
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception)
                    {
                        message = "Something went wrong. Please try again later.";
                    }
                }
                else
                {
                    message = ErrorCodeToString(createStatus);
                }
            }
            //provide an error message in case something went wrong
            model.PageMessage = new PageMessageModel
            {
                Type = PageMessageModel.MessageType.Error,
                Message = message
            };

            model.Plans.MyPlans = model.Plans.GetAllPlans(_dynabicBillingGateway);
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region Change Password
        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            ChangePasswordModel model = new ChangePasswordModel()
            {
                PageMessage = TempData["PageMessage"] as PageMessageModel ?? new PageMessageModel()
            };
            return View(model);
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    //set the Success message
                    TempData["PageMessage"] = new PageMessageModel
                    {
                        Type = PageMessageModel.MessageType.Success,
                        Message = "Your password has been changed successfully."
                    };

                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    //provide an error message in case of an Exception
                    model.PageMessage = new PageMessageModel
                    {
                        Type = PageMessageModel.MessageType.Error,
                        Message = "The current password is incorrect or the new password is invalid."
                    };
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
        #endregion

        #region Account Info
        public ActionResult AccountInfo()
        {
            //get the currently logged in user and set the Email and UserName fields in the model
            //also get the message from the TampData object
            MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
            AccountInfoModel model = new AccountInfoModel()
            {
                Email = currentUser.Email,
                PageMessage = TempData["PageMessage"] as PageMessageModel ?? new PageMessageModel()
            };
            return View(model);
        }

        //
        // POST: /Account/AccountInfo
        [HttpPost]
        public ActionResult AccountInfo(AccountInfoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //get the currently logged in user
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);

                    //if it is not the same Email as before
                    if (!currentUser.Email.Equals(model.Email))
                    {
                        //check to see if there is another user with the same email address 
                        //and throw an exception in case it does
                        var userName = Membership.GetUserNameByEmail(model.Email);
                        if (!String.IsNullOrEmpty(userName) && !currentUser.UserName.Equals(userName))
                            throw new ApplicationException("There's another user with the same Email address. Please provide a different Email address.");

                        //set the Email and update the user
                        currentUser.Email = model.Email;
                        Membership.UpdateUser(currentUser);
                    }

                    //set the Success message
                    TempData["PageMessage"] = new PageMessageModel
                    {
                        Type = PageMessageModel.MessageType.Success,
                        Message = "User " + User.Identity.Name + " has been updated successfully!"
                    };

                    return RedirectToAction("AccountInfo", "Account");
                }
                catch (Exception ex)
                {
                    //provide an error message in case of an Exception
                    model.PageMessage = new PageMessageModel
                    {
                        Type = PageMessageModel.MessageType.Error,
                        Message = ex.Message
                    };
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region Payment Info
        public ActionResult PaymentInfo()
        {
            ProfileCommon profile = ProfileCommon.GetUserProfile(User.Identity.Name);
            PaymentInfoModel model = new PaymentInfoModel(_dynabicBillingGateway, profile.CustomerReferenceId)
            {
                PageMessage = TempData["PageMessage"] as PageMessageModel ?? new PageMessageModel()
            };
            return View(model);
        }
        //
        // POST: /Account/PaymentInfo
        [HttpPost]
        public ActionResult PaymentInfo(PaymentInfoModel model, string cancel)
        {
            if (cancel != null)
            {
                return (RedirectToAction("Index", "Home"));
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Credit Card
                        CreditCardRequest creditCard = new CreditCardRequest()
                        {
                            Cvv = model.CreditCard.Cvv,
                            ExpirationDate = model.CreditCard.ExpirationDate,
                            FirstNameOnCard = model.CreditCard.FirstNameOnCard,
                            LastNameOnCard = model.CreditCard.LastNameOnCard,
                            Number = model.CreditCard.Number
                        };

                        //Billing Address
                        AddressRequest billingAddress = new AddressRequest()
                        {

                            Address1 = model.BillingAddress.BillingAddress1,
                            City = model.BillingAddress.BillingCity,
                            Country = model.BillingAddress.BillingCountry,
                            FirstName = model.BillingAddress.BillingFirstName,
                            LastName = model.BillingAddress.BillingLastName,
                            StateProvince = model.BillingAddress.BillingProvince,
                            ZipPostalCode = model.BillingAddress.BillingZipPostalCode
                        };

                        ProfileCommon profile = ProfileCommon.GetUserProfile(User.Identity.Name);

                        // add or update the Credit card
                        if (model.CreditCard.Id <= 0)
                        {
                            var customer = _dynabicBillingGateway.Customer.GetCustomerByReferenceId(Config.MySiteSubdomain, profile.CustomerReferenceId);
                            _dynabicBillingGateway.Customer.AddCreditCard(customer.Id.ToString(), creditCard);
                        }
                        else
                        {
                            _dynabicBillingGateway.Customer.UpdateCreditCardByCustomerReferenceId(Config.MySiteSubdomain,
                                profile.CustomerReferenceId,
                                model.CreditCard.Id.ToString(),
                                creditCard);
                        }

                        // add or update the Billing Address
                        if (model.BillingAddress.BillingAddressId <= 0)
                        {
                            var customer = _dynabicBillingGateway.Customer.GetCustomerByReferenceId(Config.MySiteSubdomain, profile.CustomerReferenceId);
                            _dynabicBillingGateway.Customer.AddBillingAddress(customer.Id.ToString(), billingAddress);
                        }
                        else
                        {
                            _dynabicBillingGateway.Customer.UpdateBillingAddressByCustomerReferenceId(Config.MySiteSubdomain,
                                profile.CustomerReferenceId,
                                model.BillingAddress.BillingAddressId.ToString(),
                                billingAddress);
                        }

                        //set the Success message
                        TempData["PageMessage"] = new PageMessageModel
                        {
                            Type = PageMessageModel.MessageType.Success,
                            Message = "Your Payment details have been updated successfully."
                        };

                        return RedirectToAction("PaymentInfo", "Account");
                    }
                    catch
                    {
                        //provide an error message in case something went wrong
                        model.PageMessage = new PageMessageModel
                        {
                            Type = PageMessageModel.MessageType.Error,
                            Message = "Something went wrong. Please try again later."
                        };
                    }

                }
            }
            if (model.PageMessage == null)
                model.PageMessage = new PageMessageModel();
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region Change Plan
        public ActionResult ChangePlan()
        {
            ProfileCommon profile = ProfileCommon.GetUserProfile(User.Identity.Name);
            try
            {
                var creditCard = _dynabicBillingGateway.Customer.GetFirstCreditCardForCustomerByReferenceId(profile.CustomerReferenceId);
            }
            catch (NotFoundException)
            {
                return View("FillPaymentInfoWarning", new PageMessageModel { Message = "You must set your Payment details before changing a Plan.", Type = PageMessageModel.MessageType.Warning });
            }
            ChangePlanModel model = new ChangePlanModel(_dynabicBillingGateway, profile.CurrentPlanId);
            model.Plans.SelectedPlan = profile.CurrentPlanId;
            model.PageMessage = TempData["PageMessage"] as PageMessageModel ?? new PageMessageModel();
            return View(model);
        }

        // POST: /Account/ChangePlan
        [HttpPost]
        public ActionResult ChangePlan(ChangePlanModel model)
        {
            model.PageMessage = new PageMessageModel();
            if (ModelState.IsValid)
            {
                try
                {
                    ProfileCommon profile = ProfileCommon.GetUserProfile(User.Identity.Name);
                    //get all the subscriptions of a Customer and 
                    //select the current Subscription
                    var subscriptions = _dynabicBillingGateway.Subscription.GetSubscriptionsOfCustomerByReferenceId(Config.MySiteSubdomain, profile.CustomerReferenceId.ToString());
                    var subscription = subscriptions.Where(o => o.ProductId == profile.CurrentPlanId).FirstOrDefault();

                    // get the pricing plan of the selected product
                    var product = _dynabicBillingGateway.Products.GetProductById(model.Plans.SelectedPlan.ToString());
                    if (product == null || product.PricingPlans == null || product.PricingPlans.Count == 0)
                    {
                        throw new ApplicationException("Product or product's pricing plan is not found");
                    }

                    // upgrade the subscription
                    _dynabicBillingGateway.Subscription.UpgradeDowngradeSubscriptionProduct(subscription.Id.ToString(), product.PricingPlans[0].Id.ToString(), "false", "false");

                    //update the value in Profile with the new value
                    profile.CurrentPlanId = model.Plans.SelectedPlan;

                    //set the Success message
                    TempData["PageMessage"] = new PageMessageModel
                    {
                        Type = PageMessageModel.MessageType.Success,
                        Message = "Your subscription has been successfuly updated."
                    };

                    return RedirectToAction("ChangePlan", "Account");
                }
                catch
                {
                    //provide an error message in case something went wrong
                    model.PageMessage = new PageMessageModel
                    {
                        Type = PageMessageModel.MessageType.Error,
                        Message = "Something went wrong. Please try again later."
                    };
                }
            }
            model.Plans.MyPlans = model.Plans.GetAllPlans(_dynabicBillingGateway);
            if (model.PageMessage == null)
                model.PageMessage = new PageMessageModel();
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        /// <summary>
        /// Get a specific Product through Billing Gateway;
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>A JsonResult object containing the options which will tell us 
        /// if Credit Card and Billing Addres are required at sign-up</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetSelectedPlan(int id)
        {
            //get the Product for the selected Id
            var selectedPlan = _dynabicBillingGateway.Products.GetProductById(id.ToString());

            //build and return a  Json onbject with the desired data
            JsonResult result = new JsonResult();
            result.Data = new
            {
                isCreditCardAtSignupRequired = selectedPlan.isCreditCardAtSignupRequired.ToString(),
                isBillingAddressAtSignupRequired = selectedPlan.isBillingAddressAtSignupRequired.ToString()
            };

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        #region SubscriptionDetails

        /// <summary>
        /// Displays the detailed information about the current subscription.
        /// </summary>
        /// <returns></returns>
        public ActionResult SubscriptionDetails()
        {
            var model = new SubscriptionDetailsModel();
            try
            {
                ProfileCommon profile = ProfileCommon.GetUserProfile(User.Identity.Name);
                //get all the subscriptions of a Customer and 
                //select the current Subscription
                var subscriptions = _dynabicBillingGateway.Subscription.GetSubscriptionsOfCustomerByReferenceId(Config.MySiteSubdomain, profile.CustomerReferenceId.ToString());
                var subscription = subscriptions.Where(o => o.ProductId == profile.CurrentPlanId).FirstOrDefault();

                model.SubscriptionStatus = subscription.Status;
                if (subscription.CreditCardId.HasValue && subscription.CreditCardId.Value != 0)
                {
                    var creditCard = _dynabicBillingGateway.Customer.GetCreditCard(subscription.CreditCardId.ToString());
                    model.CreditCardNumber = creditCard.Number;
                }

                model.NextAssesment = subscription.NextAssesment;

                var product = _dynabicBillingGateway.Products.GetProductById(subscription.ProductId.ToString());
                model.CurrentPlanName = product.Name;

                var pricingPlan = product.PricingPlans.FirstOrDefault(i => i.Id == subscription.ProductPricingPlanId);
                model.CurrencyCode = pricingPlan.CurrencyCode;
                if (pricingPlan.PaymentScheduleList != null && pricingPlan.PaymentScheduleList.Count > 0)
                {
                    model.MonthlyCost = string.Format("{0:0.00#####} {1}", pricingPlan.PaymentScheduleList[0].SubscriptionPeriodCharge, pricingPlan.CurrencyCode);
                }
                model.Transactions = _dynabicBillingGateway.Transaction.GetTransactionsForSubscription(subscription.Id.ToString());
            }
            catch
            {
                //provide an error message in case something went wrong
                model.PageMessage = new PageMessageModel
                {
                    Type = PageMessageModel.MessageType.Error,
                    Message = "Something went wrong. Please try again later."
                };
            }

            return View(model);
        }

        #endregion SubscriptionDetails

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
