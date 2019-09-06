using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
namespace BaseWpfMvvm_App.Common.BaseClass
{
    public class ViewModelBase : PropertyChangeBase, INotifyDataErrorInfo
    {
        protected static string ClassLogPrefix;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private string _name;
        private bool _serviceConnected;
        private bool _webHostConnected;


        protected ViewModelBase()
        {
            ServiceConnected = true;
            var type = GetType();
            Errors = new Dictionary<string, List<string>>();
        }

        /// value used to tell if the property value has been changed from its original value
        public bool IsDirty { get; set; }

        public string ServiceErrorMessage { get; set; }

        public bool ServiceError { get; set; }

        public bool IsInitializing { get; set; }

        /// value based on ERT Service connection
        public bool ServiceConnected
        {
            get => _serviceConnected;
            set => SetProperty(ref _serviceConnected, value);
        }

        /// value based on SWM Web connection
        public bool WebHostConnected
        {
            get => _webHostConnected;
            set => SetProperty(ref _webHostConnected, value);
        }

        public Dictionary<string, List<string>> Errors
        {
            get => _errors;
            set => _errors = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// value based on existence of at least one validation error
        public bool HasErrors => Errors?.Count > 0;

        /// <summary>
        ///     Event thrown when errors added or subtracted from _errors collection
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            // implement using the _errors dictionary store
            if (propertyName != null)
            {
                return Errors.ContainsKey(propertyName) ? Errors[propertyName] : null;
            }

            return Errors;
        }


        /// <summary>
        ///     Validate the property with INotifyDataErrorInfo
        ///     Set or clear _errors based on results
        ///     Trigger an ErrorsChanged Event if needed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        private void ValidateProperty<T>(string propertyName, T value)
        {
            var results = new List<ValidationResult>();

            // Data Annotations contain the concept of a Validation context
            // describes the context in which the validation is being performed
            // you can point at a given object (this)
            var context = new ValidationContext(this) { MemberName = propertyName };

            // say what member name or property on that object is being validated
            // call a method to evaluate that object
            Validator.TryValidateProperty(value, context, results);
            // results will be determined based on any data annotations
            // for that property within that object - returned in the results collection

            // integrate results returned with our dictionary
            if (results.Any()) // check if results has any elements
            {
                Errors[propertyName] = results.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                Errors.Remove(propertyName);
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public void AddError(string propertyName, string message)
        {
            var errMsgs = new List<string>
            {
                message
            };

            Errors[propertyName] = errMsgs;
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public void ClearErrors(string propertyName)
        {
            try
            {
                Errors[propertyName].Clear();
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            catch
            {
                Errors.Clear();
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        ///     Call the Base SetProperty
        ///     and then validate which will set up the _errors dictionary
        ///     and fire an Errors Changed event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member"></param>
        /// <param name="val"></param>
        /// <param name="propertyName"></param>
        protected override void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string propertyName = null)
        {
            SetProperty(ref member, val, true, propertyName);
        }
        protected void SetProperty<T>(ref T member, T val, int noErrorHandling,
            [CallerMemberName] string propertyName = null)
        {
            base.SetProperty(ref member, val, propertyName);
        }

        protected void SetProperty<T>(ref T member, T val, bool makeDirty,
            [CallerMemberName] string propertyName = null)
        {
            // call the base so INPC occurs
            base.SetProperty(ref member, val, propertyName);
            // set IsDirty flag
            if (makeDirty)
            {
                IsDirty = true;
            }
            // call the new method to trigger validation
            // using an approach that leverages data annotations
            if (!IsInitializing)
            {
                ValidateProperty(propertyName, val);
            }
            else
            {
                Errors.Clear();
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
        protected static HttpClient CreateHttpClient(string ertServiceWebApiBaseUrl)
        {
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            // Can we do this or should we use token based or some other auth
            var client = new HttpClient(handler, true) { BaseAddress = new Uri(ertServiceWebApiBaseUrl) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

    }
}
