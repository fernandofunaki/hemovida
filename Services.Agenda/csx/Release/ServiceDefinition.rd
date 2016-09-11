<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="Services.Agenda" generation="1" functional="0" release="0" Id="48d833d2-ade7-4815-bc0b-459e19924446" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="Services.AgendaGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WCFServices.Agenda:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Services.Agenda/Services.AgendaGroup/LB:WCFServices.Agenda:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="WCFServices.Agenda:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Services.Agenda/Services.AgendaGroup/MapWCFServices.Agenda:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WCFServices.AgendaInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Services.Agenda/Services.AgendaGroup/MapWCFServices.AgendaInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WCFServices.Agenda:Endpoint1">
          <toPorts>
            <inPortMoniker name="/Services.Agenda/Services.AgendaGroup/WCFServices.Agenda/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapWCFServices.Agenda:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Services.Agenda/Services.AgendaGroup/WCFServices.Agenda/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWCFServices.AgendaInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Services.Agenda/Services.AgendaGroup/WCFServices.AgendaInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="WCFServices.Agenda" generation="1" functional="0" release="0" software="C:\TCC\Project\Services.Agenda\csx\Release\roles\WCFServices.Agenda" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WCFServices.Agenda&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WCFServices.Agenda&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Services.Agenda/Services.AgendaGroup/WCFServices.AgendaInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Services.Agenda/Services.AgendaGroup/WCFServices.AgendaUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Services.Agenda/Services.AgendaGroup/WCFServices.AgendaFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="WCFServices.AgendaUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="WCFServices.AgendaFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="WCFServices.AgendaInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="4cc75712-eb59-4b93-a444-43445b6165f4" ref="Microsoft.RedDog.Contract\ServiceContract\Services.AgendaContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="9c3495e3-b2d0-43ad-8e0c-21c0b0d1480f" ref="Microsoft.RedDog.Contract\Interface\WCFServices.Agenda:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Services.Agenda/Services.AgendaGroup/WCFServices.Agenda:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>