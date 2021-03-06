﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfDecryptorService.ValidatorProxy {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://facade.validator.cesi.com/", ConfigurationName="ValidatorProxy.ValidatorEndpoint")]
    public interface ValidatorEndpoint {
        
        // CODEGEN : La génération du contrat de message depuis le nom d'élément msg de l'espace de noms  n'est pas marqué nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://facade.validator.cesi.com/ValidatorEndpoint/validatorOperationRequest", ReplyAction="http://facade.validator.cesi.com/ValidatorEndpoint/validatorOperationResponse")]
        WcfDecryptorService.ValidatorProxy.validatorOperationResponse validatorOperation(WcfDecryptorService.ValidatorProxy.validatorOperationRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class validatorOperationRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="validatorOperation", Namespace="http://facade.validator.cesi.com/", Order=0)]
        public WcfDecryptorService.ValidatorProxy.validatorOperationRequestBody Body;
        
        public validatorOperationRequest() {
        }
        
        public validatorOperationRequest(WcfDecryptorService.ValidatorProxy.validatorOperationRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class validatorOperationRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string msg;
        
        public validatorOperationRequestBody() {
        }
        
        public validatorOperationRequestBody(string msg) {
            this.msg = msg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class validatorOperationResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="validatorOperationResponse", Namespace="http://facade.validator.cesi.com/", Order=0)]
        public WcfDecryptorService.ValidatorProxy.validatorOperationResponseBody Body;
        
        public validatorOperationResponse() {
        }
        
        public validatorOperationResponse(WcfDecryptorService.ValidatorProxy.validatorOperationResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class validatorOperationResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string message;
        
        public validatorOperationResponseBody() {
        }
        
        public validatorOperationResponseBody(string message) {
            this.message = message;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ValidatorEndpointChannel : WcfDecryptorService.ValidatorProxy.ValidatorEndpoint, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ValidatorEndpointClient : System.ServiceModel.ClientBase<WcfDecryptorService.ValidatorProxy.ValidatorEndpoint>, WcfDecryptorService.ValidatorProxy.ValidatorEndpoint {
        
        public ValidatorEndpointClient() {
        }
        
        public ValidatorEndpointClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ValidatorEndpointClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ValidatorEndpointClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ValidatorEndpointClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WcfDecryptorService.ValidatorProxy.validatorOperationResponse WcfDecryptorService.ValidatorProxy.ValidatorEndpoint.validatorOperation(WcfDecryptorService.ValidatorProxy.validatorOperationRequest request) {
            return base.Channel.validatorOperation(request);
        }
        
        public string validatorOperation(string msg) {
            WcfDecryptorService.ValidatorProxy.validatorOperationRequest inValue = new WcfDecryptorService.ValidatorProxy.validatorOperationRequest();
            inValue.Body = new WcfDecryptorService.ValidatorProxy.validatorOperationRequestBody();
            inValue.Body.msg = msg;
            WcfDecryptorService.ValidatorProxy.validatorOperationResponse retVal = ((WcfDecryptorService.ValidatorProxy.ValidatorEndpoint)(this)).validatorOperation(inValue);
            return retVal.Body.message;
        }
    }
}
