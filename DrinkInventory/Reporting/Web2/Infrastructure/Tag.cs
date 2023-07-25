using System.Web.Mvc;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public class Tag : TagBuilder
    {
        public Tag(string _tagName) : base(_tagName)
        {
        }

        public MvcHtmlString ToHtml()
        {
            return MvcHtmlString.Create(ToString());
        }
        
        public Tag AppendInnerHtml(object _html)
        {
            base.InnerHtml += _html;
            return this;
        }

        public new Tag InnerHtml(object _html)
        {
            base.InnerHtml = _html.ToString();
            return this;
        }

        public Tag Attribute(string _key, object _value)
        {
            Attributes.Add(_key, _value.ToString());
            return this;
        }

        public Tag Class(string _value)
        {
            AddCssClass(_value);
            return this;
        }

        public Tag Id(object _value)
        {
            Attribute("ID", _value);
            return this;
        }

        public Tag Href(string _value)
        {
            return Attribute("href", _value);
        }

        public Tag Alt(string _value)
        {
            return Attribute("alt", _value);
        }

        public Tag Style(string _value)
        {
            return Attribute("style", _value);
        }

        public Tag For(object _value)
        {
            return Attribute("for", _value);
        }

        public Tag Type(string _value)
        {
            return Attribute("type", _value);
        }

        public Tag Value(string _value)
        {
            return Attribute("value", _value);
        }

        public Tag Name(object _value)
        {
            return Attribute("name", _value);
        }

        public Tag OnChange(string _value)
        {
            return Attribute("onchange", _value);
        }

        public Tag OnKeyDown(string _value)
        {
            return Attribute("onkeydown", _value);
        }

        public Tag Checked(object _value)
        {
            return Attribute("checked", _value);
        }
    }

    public class Div : Tag { public Div() : base("div") { } }
    public class A : Tag { public A() : base("a") { } }
    public class Span : Tag { public Span() : base("span") { } }
    public class Ul : Tag { public Ul() : base("ul") { } }
    public class Li : Tag { public Li() : base("li") { } }
    public class Nav : Tag { public Nav() : base("nav") { } }
    public class Table : Tag { public Table() : base("table") { } }
    public class Tr : Tag { public Tr() : base("tr") { } }
    public class Th : Tag { public Th() : base("th") { } }
    public class Td : Tag { public Td() : base("td") { } }
    public class Input : Tag { public Input() : base("input") { } }
    public class Label : Tag { public Label() : base("label") { } }
    public class Form : Tag { public Form() : base("form") { } }
}
