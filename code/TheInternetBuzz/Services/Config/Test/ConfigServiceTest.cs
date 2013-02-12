using System;
using System.Collections;
using System.Reflection;

using NUnit.Framework;

namespace TheInternetBuzz.Services.Config.Test
{
    [TestFixture]
    public class ConfigServiceTest
    {
        [Test]
        public void GetUnknowConfig()
        {
            int config = ConfigService.GetConfig("unknown", 0);
            Assert.That(config == 0);
        }

        [Test]
        public void GetConfigVersion()
        {
            int version = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_VERSION_MAJOR, 0);
            Assert.That(version > 0);
        }

        [Test]
        public void CheckAllConstants()
        {
            FieldInfo[] fieldInfos = typeof(ConfigKeys).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (FieldInfo fieldinfo in fieldInfos)
            {
                String key = fieldinfo.GetValue(fieldinfo).ToString();
                string value = ConfigService.GetConfig(key, "");

                Assert.That(value != null);
                Assert.That(value.Length > 0);
            }
        }
    }
}