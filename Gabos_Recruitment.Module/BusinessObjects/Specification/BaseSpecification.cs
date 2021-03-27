﻿using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gabos_Recruitment.Module.BusinessObjects.Specification
{
    [DomainComponent]
    [DefaultClassOptions]
    public abstract class BaseSpecification : BaseNonPersistentObject, IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        public BaseSpecification()
        {
            Oid = Guid.NewGuid();
        }

        private string number;
        [RuleRequiredField(CustomMessageTemplate = "Pole nie może być puste")]
        [RuleRegularExpression("^[0-9]+$", CustomMessageTemplate = "Pole zawiera niedozwolone znaki. Wymagane tylko cyfry.")]
        public string Number
        {
            get { return number; }
            set
            {
                number = value;
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