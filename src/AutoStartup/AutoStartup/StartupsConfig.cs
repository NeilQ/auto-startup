﻿using System.Configuration;

namespace AutoStartup
{
    public class StartupElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("path", IsRequired = true, IsKey = false)]
        public string Path
        {
            get { return (string)this["path"]; }
            set { this["path"] = value; }
        }

        [ConfigurationProperty("args")]
        public string Args
        {
            get { return (string)this["args"]; }
            set { this["args"] = value; }
        }
    }

    public class StartupCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new StartupElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((StartupElement)element).Name;
        }

        public StartupElement this[int index]
        {
            get
            {
                return (StartupElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new StartupElement this[string name]
        {
            get
            {
                return (StartupElement)BaseGet(name);
            }
        }
    }

    public class StartupSection : ConfigurationSection
    {
        private static readonly StartupSection settings =
        ConfigurationManager.GetSection("autoStartup") as StartupSection;

        public static StartupSection Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty("startups", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(StartupCollection),
              AddItemName = "add",
              ClearItemsName = "clear",
              RemoveItemName = "remove")]
        public StartupCollection Startups
        {
            get { return (StartupCollection)base["startups"]; }
        }

    }
}