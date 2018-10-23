using System;
using System.Collections.Generic;
using RadiusCSharp.Core.DataAccess;
using WineMS.Common.Configuration;
using Xunit;

namespace WineMS.UnitTests.DataAccess {

  public class TestEvolutionConnectionStrings : IDisposable {
    private const string CompanyId1 = "com-id-1";
    private const string CompanyId2 = "com-id-2";

    private const string ExpectedCompanyDatabaseForDefault = "company-default";
    private const string ExpectedCommonDatabaseForDefault = "common-default";

    private const string ExpectedCompanyDatabaseForCompanyId1 = "company-company-id-1";
    private const string ExpectedCompanyDatabaseForCompanyId2 = "company-company-id-2";
    private const string ExpectedCommonDatabaseForCompanyId1 = "common-company-id-1";

    public TestEvolutionConnectionStrings()
    {
      var options = new Dictionary<string, object> {
        {"EvolutionCompanyDatabase-" + CompanyId1, ExpectedCompanyDatabaseForCompanyId1},
        {"EvolutionCompanyDatabase-" + CompanyId2, ExpectedCompanyDatabaseForCompanyId2},
        {"EvolutionCompanyDatabase-default", ExpectedCompanyDatabaseForDefault},
        {"EvolutionCommonDatabase-" + CompanyId1, ExpectedCommonDatabaseForCompanyId1},
        {"EvolutionCommonDatabase-default", ExpectedCommonDatabaseForDefault},
      };
      
      KeyValueFunctions.GetKeyValueProvider = keyName => options[keyName];
      KeyValueFunctions.GetKeyNameExistsProvider = keyName => options.ContainsKey(keyName);
    }

    public void Dispose() { }


    [Fact]
    public void GetCompanyConnectionStringsForCompanyId1()
    {
      var connectionStrings = CompanyId1.GetEvolutionConnectionStrings();
      Assert.Equal(ExpectedCommonDatabaseForCompanyId1, connectionStrings.CommonDatabase);
      Assert.Equal(ExpectedCompanyDatabaseForCompanyId1, connectionStrings.CompanyDatabase);
    }

    [Fact]
    public void GetCompanyConnectionStringsForDefault()
    {
      var connectionStrings = "invalid-company-id".GetEvolutionConnectionStrings();
      Assert.Equal(ExpectedCommonDatabaseForDefault, connectionStrings.CommonDatabase);
      Assert.Equal(ExpectedCompanyDatabaseForDefault, connectionStrings.CompanyDatabase);
    }

    [Fact]
    public void GetCompanyConnectionStringsForCompanyId2CompanyDefaultCommon()
    {
      var connectionStrings = CompanyId2.GetEvolutionConnectionStrings();
      Assert.Equal(ExpectedCommonDatabaseForDefault, connectionStrings.CommonDatabase);
      Assert.Equal(ExpectedCompanyDatabaseForCompanyId2, connectionStrings.CompanyDatabase);
    }


  }

}