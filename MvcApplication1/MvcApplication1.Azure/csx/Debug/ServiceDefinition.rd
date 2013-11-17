﻿<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="MvcApplication1.Azure" generation="1" functional="0" release="0" Id="b73b39e1-a3de-4144-9d61-52c338530f98" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="MvcApplication1.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="MvcApplication1:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MvcApplication1.Azure/MvcApplication1.AzureGroup/LB:MvcApplication1:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="MvcApplication1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MvcApplication1.Azure/MvcApplication1.AzureGroup/MapMvcApplication1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="MvcApplication1Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MvcApplication1.Azure/MvcApplication1.AzureGroup/MapMvcApplication1Instances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:MvcApplication1:Endpoint1">
          <toPorts>
            <inPortMoniker name="/MvcApplication1.Azure/MvcApplication1.AzureGroup/MvcApplication1/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapMvcApplication1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MvcApplication1.Azure/MvcApplication1.AzureGroup/MvcApplication1/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapMvcApplication1Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MvcApplication1.Azure/MvcApplication1.AzureGroup/MvcApplication1Instances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="MvcApplication1" generation="1" functional="0" release="0" software="c:\users\cbingh\documents\visual studio 2012\Projects\MvcApplication1\MvcApplication1.Azure\csx\Debug\roles\MvcApplication1" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;MvcApplication1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;MvcApplication1&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MvcApplication1.Azure/MvcApplication1.AzureGroup/MvcApplication1Instances" />
            <sCSPolicyUpdateDomainMoniker name="/MvcApplication1.Azure/MvcApplication1.AzureGroup/MvcApplication1UpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MvcApplication1.Azure/MvcApplication1.AzureGroup/MvcApplication1FaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="MvcApplication1UpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="MvcApplication1FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="MvcApplication1Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="4d975549-87c1-452a-835b-409f4a236db8" ref="Microsoft.RedDog.Contract\ServiceContract\MvcApplication1.AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="f1b438b2-6cab-41da-8259-5c299b0db041" ref="Microsoft.RedDog.Contract\Interface\MvcApplication1:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MvcApplication1.Azure/MvcApplication1.AzureGroup/MvcApplication1:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>