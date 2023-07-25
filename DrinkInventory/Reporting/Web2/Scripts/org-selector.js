$(document).ready(function ()
{
    function OrgSelector(element)
    {
        // given an element in the DOM that is a descendant of the root or the actual root, return the OrgSelector object
        function getSelector(element)
        {
            root = getRootElement(element);
            return root.data('orgSel');
        }

        // given a child element of the root element or the root itself, return the root element div
        function getRootElement(element)
        {
            if ($(element).hasClass('orgtree'))
                return element;
            return (getRootElement($(element).parent()));
        }

        // the selected organization.  starts out as null.
        this.selectedOrg = null;

        // update the organization in the DOM
        this.updateOrg = function (org)
        {
            var orgNode = this.getOrgNode(org.OrganizationId);
            $(orgNode).data('org', org);
            $(orgNode).children('.orgtreeitemtitle').text(org.Name);
        }

        // remove an organization from the tree
        this.removeOrg = function (orgId)
        {
            var orgNode = this.getOrgNode(orgId);
            $(orgNode).remove();
        }

        // get the parent organization using the tree in the DOM
        this.getParentOrg = function (orgId)
        {
            var orgNode = this.getOrgNode(orgId);
            var parentNode = $(orgNode).parent();
            var parentOrg = $(parentNode).data('org');
            return parentOrg;
        }

        // expand organization node
        this.expandOrg = function (orgId, completed)
        {
            var orgNode = this.getOrgNode(orgId);
            var orgSel = this;

            this.populateOrgNode(orgId, orgNode, function ()
            {
                $(orgNode).data('expanded', true);
                if ($(orgNode).children('.orgtreeitem').length > 0)
                {
                    $(orgNode).children('img').attr('src', orgSel.collapseIconUrl());
                }
                else
                {
                    $(orgNode).children('img').attr('src', orgSel.emptyIconUrl());
                }

                completed();
            });
        }

        // get node (div) in the DOM for the give organzation
        this.getOrgNode = function (orgId)
        {
            var orgNode = $(this.rootElement).find('#o' + orgId + ':first');
            return orgNode;
        }

        this.loadOrg = function (org, complete)
        {
            loadOrgInternal(org, complete, this);
        }

        function loadOrgInternal(org, complete, orgSel)
        {
            var orgNode = orgSel.getOrgNode(org.OrganizationId);

            if (orgNode.length > 0)
            {
                complete();
            }
            else
            {
                callServer(orgSel.getOrganizationUrl(), { _organizationId: org.ParentId }, 'GET', function (data)
                {
                    var parentOrg = data.Organization;
                    loadOrgInternal(parentOrg, function ()
                    {
                        orgSel.expandOrg(org.ParentId, function ()
                        {
                            complete();
                        });
                    }, orgSel);
                });
            }
        }

        // select the organization in the DOM
        this.selectOrg = function (orgId)
        {
            // unhighlight currently selected org
            $(this.rootElement).find('.orgtreeitemtitle').animate({ 'color': 'Black', 'backgroundColor': 'White' },
                { queue: false, duration: 300 });

            var foundOrg = null;
            if (orgId != null)
            {
                // find the org element in the DOM
                var orgNode = this.getOrgNode(orgId);
                var title = $(orgNode).children('.orgtreeitemtitle');
                // highlight the title
                $(title).animate({ backgroundColor: '#0000ff', color: 'White' }, { queue: false, duration: 300 });
                foundOrg = $(orgNode).data('org');
            }

            this.selectedOrg = foundOrg;
        }

        // click handler for the expand icon image of an orgtreeitem element
        function onClickExpand()
        {
            var sel = getSelector(this);
            var orgNode = $(this).parent();
            var expanded = orgNode.data('expanded');

            if (!expanded)
            {
                sel.expandOrg($(orgNode).data('org').OrganizationId, function () { });
            }
            else
            {
                $(orgNode).data('expanded', false);
                $(orgNode).children('.orgtreeitem').remove();
                $(this).attr('src', sel.expandIconUrl());
            }
        }

        // click handler for the orgtreeitem title element
        function onClickOrgTitle()
        {
            var orgSel = getSelector(this);
            orgSel.selectOrg($(this).parent().data('org').OrganizationId);
            $(orgSel.rootElement).trigger('selectionChanged');
        }

        // build an organization element node for the given org data
        this.buildOrgNode = function (org)
        {
            var name = org.Name == '' || org.Name == undefined ? '[empty]' : org.Name;
            var title = $('<span class="orgtreeitemtitle">' + name + '</span>').click(onClickOrgTitle);
            var icon = $('<img src="' + this.expandIconUrl() + '" />').click(onClickExpand);
            var orgNode = $('<div class=\"orgtreeitem\" id=\"o' + org.OrganizationId + '"></div>').data('org', org);
            $(icon).appendTo(orgNode);
            $(title).appendTo(orgNode);
            return orgNode;
        }

        // shortcut to get the URL for the server method to get the organization list
        this.getOrganizationsUrl = function ()
        {
            return $(this.rootElement).children('.getOrganizationsUrl').text();
        }

        // shortcut to get the URL for the server method to get a specific organization
        this.getOrganizationUrl = function ()
        {
            return $(this.rootElement).children('.getOrganizationUrl').text();
        }

        // shortcut to get the URL for the expand icon
        this.expandIconUrl = function ()
        {
            return $(this.rootElement).children('.expandIconUrl').text();
        }

        // shortcut to get the URL for the collapse icon
        this.collapseIconUrl = function ()
        {
            return $(this.rootElement).children('.collapseIconUrl').text();
        }

        // shortcut to get the URL for the expand icon
        this.emptyIconUrl = function ()
        {
            return $(this.rootElement).children('.emptyIconUrl').text();
        }

        // populate an organization node
        this.populateOrgNode = function (orgId, parentElement, completed)
        {
            var orgSel = this;
            callServer(this.getOrganizationsUrl(), { _organizationId: orgId }, 'GET', function (data)
            {
                $(parentElement).children('.orgtreeitem').remove();
                $.each(data, function ()
                {
                    var orgNode = orgSel.buildOrgNode(this);
                    $(orgNode).appendTo(parentElement);
                });
                completed();
            });
        }

        this.rootElement = element;
        $(element).data('orgSel', this);
        this.populateOrgNode('', element, function () { });
    }

    $('.orgtree').each(function ()
    {
        new OrgSelector(this);
    });
});
