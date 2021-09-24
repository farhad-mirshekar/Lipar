using Lipar.Core;
using Lipar.Core.Infrastructure;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.General.Contracts;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Lipar.Entities.Domain.Organization.Enums;

namespace Lipar.Web.Framework.Menu
{
    public class XmlSiteMap
    {
        public XmlSiteMap()
        {
            RootNode = new SiteMapNode();
        }
        public SiteMapNode RootNode { get; set; }

        public virtual void LoadFrom(string physicalPath)
        {
            var fileProvider = EngineContext.Current.Resolve<ILiparFileProvider>();

            var filePath = fileProvider.MapPath(physicalPath);
            var content = fileProvider.ReadAllText(filePath, Encoding.UTF8);

            if (!string.IsNullOrEmpty(content))
            {
                var doc = new XmlDocument();
                using (var sr = new StringReader(content))
                {
                    using var xr = XmlReader.Create(sr,
                        new XmlReaderSettings
                        {
                            CloseInput = true,
                            IgnoreWhitespace = true,
                            IgnoreComments = true,
                            IgnoreProcessingInstructions = true
                        });

                    doc.Load(xr);
                }
                if ((doc.DocumentElement != null) && doc.HasChildNodes)
                {
                    var xmlRootNode = doc.DocumentElement.FirstChild;
                    Iterate(RootNode, xmlRootNode);
                }
            }
        }

        private static void Iterate(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            PopulateNode(siteMapNode, xmlNode);

            foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
            {
                if (xmlChildNode.LocalName.Equals("siteMapNode", StringComparison.InvariantCultureIgnoreCase))
                {
                    var siteMapChildNode = new SiteMapNode();
                    siteMapNode.ChildNodes.Add(siteMapChildNode);

                    Iterate(siteMapChildNode, xmlChildNode);
                }
            }
        }

        private static void PopulateNode(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            //system name
            siteMapNode.SystemName = GetStringValueFromAttribute(xmlNode, "SystemName");

            //title
            var liparResource = GetStringValueFromAttribute(xmlNode, "liparResource");
            var _localeStringResourceService = EngineContext.Current.Resolve<ILocaleStringResourceService>();
            siteMapNode.Title = _localeStringResourceService.GetResource(liparResource);

            //routes, url
            var controllerName = GetStringValueFromAttribute(xmlNode, "controller");
            var actionName = GetStringValueFromAttribute(xmlNode, "action");
            var url = GetStringValueFromAttribute(xmlNode, "url");
            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
            {
                siteMapNode.ControllerName = controllerName;
                siteMapNode.ActionName = actionName;

                siteMapNode.RouteValues = new RouteValueDictionary { { "area", AreaNames.Admin } };
            }
            else if (!string.IsNullOrEmpty(url))
            {
                siteMapNode.Url = url;
            }

            //image URL
            siteMapNode.IconClass = GetStringValueFromAttribute(xmlNode, "IconClass");

            //permission name
            var permissionNames = GetStringValueFromAttribute(xmlNode, "PermissionNames");
            if (!string.IsNullOrEmpty(permissionNames))
            {
                var _commandService = EngineContext.Current.Resolve<ICommandService>();
                var _workContext = EngineContext.Current.Resolve<IWorkContext>();

                if (_workContext.CurrentPosition != null)
                {
                    var commands = _commandService.List(new CommandListVM { RoleId = _workContext.CurrentPosition.PositionRoles.Select(r => r.RoleId).First() });

                    siteMapNode.Visible = commands.Any(c => (c.CommandType.Title + "." + c.SystemName).ToLower().Equals(permissionNames.ToLower()));
                }

            }
            else
            {
                siteMapNode.Visible = true;
            }

            // Open URL in new tab
            var openUrlInNewTabValue = GetStringValueFromAttribute(xmlNode, "OpenUrlInNewTab");
            if (!string.IsNullOrWhiteSpace(openUrlInNewTabValue) && bool.TryParse(openUrlInNewTabValue, out var booleanResult))
            {
                siteMapNode.OpenUrlInNewTab = booleanResult;
            }
        }

        private static string GetStringValueFromAttribute(XmlNode node, string attributeName)
        {
            string value = null;

            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                var attribute = node.Attributes[attributeName];

                if (attribute != null)
                {
                    value = attribute.Value;
                }
            }

            return value;
        }
    }
}
