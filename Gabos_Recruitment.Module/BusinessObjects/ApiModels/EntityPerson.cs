using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gabos_Recruitment.Module.BusinessObjects.ApiModels
{
    [DomainComponent]
    [DefaultClassOptions]
    public class EntityPerson : BaseNonPersistentObject, IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        public EntityPerson()
        {
            Oid = Guid.NewGuid();
        }

        private string companyName;
        public string CompanyName
        {
            get { return companyName; }
            set
            {
                companyName = value;
                OnPropertyChanged();
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        private string pesel;
        public string Pesel
        {
            get { return pesel; }
            set
            {
                pesel = value;
                OnPropertyChanged();
            }
        }

        private string nip;
        public string Nip
        {
            get { return nip; }
            set
            {
                nip = value;
                OnPropertyChanged();
            }
        }

        #region IObjectSpaceLink members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIObjectSpaceLinktopic.aspx)
        // Use the Object Space to access other entities from IXafEntityObject methods (see https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113707.aspx).
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion
    }
}
