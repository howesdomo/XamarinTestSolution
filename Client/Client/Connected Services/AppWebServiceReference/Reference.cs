//------------------------------------------------------------------------------
// <自动生成>
//     此代码由工具生成。
//     //
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </自动生成>
//------------------------------------------------------------------------------

namespace AppWebServiceReference
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Order", Namespace="http://tempuri.org/")]
    public partial class Order : object
    {
        
        private string OrderNoField;
        
        private System.DateTime CreateTimeField;
        
        private System.Nullable<System.DateTime> EndTimeField;
        
        private decimal PlanQtyField;
        
        private System.Nullable<decimal> ScanQtyField;
        
        private System.Collections.Generic.List<AppWebServiceReference.Carton> CartonListField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string OrderNo
        {
            get
            {
                return this.OrderNoField;
            }
            set
            {
                this.OrderNoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public System.DateTime CreateTime
        {
            get
            {
                return this.CreateTimeField;
            }
            set
            {
                this.CreateTimeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public System.Nullable<System.DateTime> EndTime
        {
            get
            {
                return this.EndTimeField;
            }
            set
            {
                this.EndTimeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public decimal PlanQty
        {
            get
            {
                return this.PlanQtyField;
            }
            set
            {
                this.PlanQtyField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public System.Nullable<decimal> ScanQty
        {
            get
            {
                return this.ScanQtyField;
            }
            set
            {
                this.ScanQtyField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public System.Collections.Generic.List<AppWebServiceReference.Carton> CartonList
        {
            get
            {
                return this.CartonListField;
            }
            set
            {
                this.CartonListField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Carton", Namespace="http://tempuri.org/")]
    public partial class Carton : object
    {
        
        private string CartonNoField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CartonNo
        {
            get
            {
                return this.CartonNoField;
            }
            set
            {
                this.CartonNoField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SearchArgs", Namespace="http://tempuri.org/")]
    public partial class SearchArgs : object
    {
        
        private System.Nullable<int> OrderCountField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Nullable<int> OrderCount
        {
            get
            {
                return this.OrderCountField;
            }
            set
            {
                this.OrderCountField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AppWebServiceReference.AppWebServiceSoap")]
    public interface AppWebServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<AppWebServiceReference.HelloWorldResponse> HelloWorldAsync(AppWebServiceReference.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetOrder", ReplyAction="*")]
        System.Threading.Tasks.Task<AppWebServiceReference.GetOrderResponse> GetOrderAsync(AppWebServiceReference.GetOrderRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetOrderList", ReplyAction="*")]
        System.Threading.Tasks.Task<AppWebServiceReference.GetOrderListResponse> GetOrderListAsync(AppWebServiceReference.GetOrderListRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/", Order=0)]
        public AppWebServiceReference.HelloWorldRequestBody Body;
        
        public HelloWorldRequest()
        {
        }
        
        public HelloWorldRequest(AppWebServiceReference.HelloWorldRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody
    {
        
        public HelloWorldRequestBody()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/", Order=0)]
        public AppWebServiceReference.HelloWorldResponseBody Body;
        
        public HelloWorldResponse()
        {
        }
        
        public HelloWorldResponse(AppWebServiceReference.HelloWorldResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody()
        {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult)
        {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetOrderRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetOrder", Namespace="http://tempuri.org/", Order=0)]
        public AppWebServiceReference.GetOrderRequestBody Body;
        
        public GetOrderRequest()
        {
        }
        
        public GetOrderRequest(AppWebServiceReference.GetOrderRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetOrderRequestBody
    {
        
        public GetOrderRequestBody()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetOrderResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetOrderResponse", Namespace="http://tempuri.org/", Order=0)]
        public AppWebServiceReference.GetOrderResponseBody Body;
        
        public GetOrderResponse()
        {
        }
        
        public GetOrderResponse(AppWebServiceReference.GetOrderResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetOrderResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public AppWebServiceReference.Order GetOrderResult;
        
        public GetOrderResponseBody()
        {
        }
        
        public GetOrderResponseBody(AppWebServiceReference.Order GetOrderResult)
        {
            this.GetOrderResult = GetOrderResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetOrderListRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetOrderList", Namespace="http://tempuri.org/", Order=0)]
        public AppWebServiceReference.GetOrderListRequestBody Body;
        
        public GetOrderListRequest()
        {
        }
        
        public GetOrderListRequest(AppWebServiceReference.GetOrderListRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetOrderListRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public AppWebServiceReference.SearchArgs args;
        
        public GetOrderListRequestBody()
        {
        }
        
        public GetOrderListRequestBody(AppWebServiceReference.SearchArgs args)
        {
            this.args = args;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetOrderListResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetOrderListResponse", Namespace="http://tempuri.org/", Order=0)]
        public AppWebServiceReference.GetOrderListResponseBody Body;
        
        public GetOrderListResponse()
        {
        }
        
        public GetOrderListResponse(AppWebServiceReference.GetOrderListResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetOrderListResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Collections.Generic.List<AppWebServiceReference.Order> GetOrderListResult;
        
        public GetOrderListResponseBody()
        {
        }
        
        public GetOrderListResponseBody(System.Collections.Generic.List<AppWebServiceReference.Order> GetOrderListResult)
        {
            this.GetOrderListResult = GetOrderListResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface AppWebServiceSoapChannel : AppWebServiceReference.AppWebServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class AppWebServiceSoapClient : System.ServiceModel.ClientBase<AppWebServiceReference.AppWebServiceSoap>, AppWebServiceReference.AppWebServiceSoap
    {
        
    /// <summary>
    /// 实现此分部方法，配置服务终结点。
    /// </summary>
    /// <param name="serviceEndpoint">要配置的终结点</param>
    /// <param name="clientCredentials">客户端凭据</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public AppWebServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(AppWebServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), AppWebServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AppWebServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(AppWebServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AppWebServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(AppWebServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AppWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AppWebServiceReference.HelloWorldResponse> AppWebServiceReference.AppWebServiceSoap.HelloWorldAsync(AppWebServiceReference.HelloWorldRequest request)
        {
            return base.Channel.HelloWorldAsync(request);
        }
        
        public System.Threading.Tasks.Task<AppWebServiceReference.HelloWorldResponse> HelloWorldAsync()
        {
            AppWebServiceReference.HelloWorldRequest inValue = new AppWebServiceReference.HelloWorldRequest();
            inValue.Body = new AppWebServiceReference.HelloWorldRequestBody();
            return ((AppWebServiceReference.AppWebServiceSoap)(this)).HelloWorldAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AppWebServiceReference.GetOrderResponse> AppWebServiceReference.AppWebServiceSoap.GetOrderAsync(AppWebServiceReference.GetOrderRequest request)
        {
            return base.Channel.GetOrderAsync(request);
        }
        
        public System.Threading.Tasks.Task<AppWebServiceReference.GetOrderResponse> GetOrderAsync()
        {
            AppWebServiceReference.GetOrderRequest inValue = new AppWebServiceReference.GetOrderRequest();
            inValue.Body = new AppWebServiceReference.GetOrderRequestBody();
            return ((AppWebServiceReference.AppWebServiceSoap)(this)).GetOrderAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AppWebServiceReference.GetOrderListResponse> AppWebServiceReference.AppWebServiceSoap.GetOrderListAsync(AppWebServiceReference.GetOrderListRequest request)
        {
            return base.Channel.GetOrderListAsync(request);
        }
        
        public System.Threading.Tasks.Task<AppWebServiceReference.GetOrderListResponse> GetOrderListAsync(AppWebServiceReference.SearchArgs args)
        {
            AppWebServiceReference.GetOrderListRequest inValue = new AppWebServiceReference.GetOrderListRequest();
            inValue.Body = new AppWebServiceReference.GetOrderListRequestBody();
            inValue.Body.args = args;
            return ((AppWebServiceReference.AppWebServiceSoap)(this)).GetOrderListAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.AppWebServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.AppWebServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.AppWebServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:17911/AppWebApplication461/AppWebService.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.AppWebServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:17911/AppWebApplication461/AppWebService.asmx");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            AppWebServiceSoap,
            
            AppWebServiceSoap12,
        }
    }
}
