using Nop.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc;
using Autofac.Core;
using Autofac;
using Nop.Data;
using Nop.Core.Data;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.ProductEditor.Filters;
using Nop.Plugin.Misc.ProductEditor.Extensions;
using Nop.Services.Catalog;

namespace Nop.Plugin.Misc.ProductEditor.Infrastructure
{
    /*
    -  NopCommerce uporablja constructor injection z uporabo AutoFac-a
    -  samo deklariramo parametre od interfaceov ali class types, ki jih bomo rabili v našem class konstruktoreju.
     * Autofac pa bo poskrbel za instance teh tipov in managiranje njihovih lifeCycle-ov.
     * Autofac bo vedel, kako to narediti, glede na pravila, ki jih definiramo v našem dependenc registrar classu
     * We can simply declare parameters of the interface or class types that we need in our class constructors. Autofac will handle creating instances of these types and managing their lifecycle. Autofac will understand how to do this based on the rules that we defined in our DependencyRegistrar class. 
     For example, our main plugin class is going to require an instance of our ObjectContext and a repository for our slider records. We can simply ask for these types in the constructor and then use them throughout our class, knowing that Autofac will make sure that they're provided. Notice that this also works with the generic repository. We could ask for an IRepository of any type, as long as it's within the scope of our plugin and registered with Autofac
    */

    /*tule definiramo class za manage our dependency injection*/
    
    public class ProductEditorDependencyRegistrar: IDependencyRegistrar
    {
        //dodamo konstanto
        //private const string CONTEXT_NAME = "nop_object_context_product_editor";

        //nop commerce gre čez vse naše plugin dll-je in poišče classe, ki imlementirajo IDependencyRegistrar in pokliče register metodo na vsakem od teh 
        //So what NopCommerce does is it goes through all of our plugin dll's and find classes that implement IDependencyRegistrar, and it calls this Register method on each of those. So the Order property just determines what order they get registered in. For our plugin this doesn't really matter, so I'll just have it return a value of 1 for now
        
        public int Order
        => 1;
        
        /*
        public int Order//samo pove, po katerem vrstnem redu naj registrira - za naš plugin to ni pommebno, zato naj vrne 1
        {
            get { return 1; }
        }
        */
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<ProductEditorServicePlugin>();
            builder.RegisterType<PriceCalculation>().As<IPriceCalculationService>();
            builder.RegisterType<CompressFilter>().As<System.Web.Mvc.IFilterProvider>();
        }

        //public void Register(Autofac.ContainerBuilder builder, Core.Infrastructure.ITypeFinder typeFinder, NopConfig config)
        //{
        //    builder.RegisterType<ProductEditorServicePlugin>();
        //    //throw new NotImplementedException();
        //}
    }
    
}
