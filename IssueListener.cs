using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Countersoft.Gemini.Commons.Entity;
using Countersoft.Gemini.Extensibility.Events;
using Countersoft.Gemini.Commons.Dto;
using Countersoft.Gemini.Extensibility.Apps;

namespace AutoOpenClosedItem
{
    [AppType(AppTypeEnum.Event),
    AppGuid("03A51F30-91C1-47D5-9F80-31A101FDF5C4"),
    AppName("Auto Open Closed Item"),
    AppDescription("Automatically opens closed item when it is updated")]
    public class IssueListener : IIssueBeforeListener
    {
        public Issue BeforeCreate(IssueEventArgs args)
        {
            return args.Entity;
        }

        public Issue BeforeUpdate(IssueEventArgs args)
        {
            // We have changed status
            if(args.Entity.StatusId != args.Previous.StatusId) return args.Entity;
            
            var status = args.Context.Meta.StatusGet(args.Entity.StatusId);

            // Status not found? or not closed
            if (status == null || !status.IsFinal) return args.Entity;

            // Set the status to open
            var project = args.Context.Projects.Get(args.Entity.ProjectId);
            
            if(project == null) return args.Entity;
            
            var statuses = args.Context.Meta.StatusGet().FindAll( s=> s.TemplateId == project.TemplateId);
            
            if(statuses.Count == 0) return args.Entity;
            
            foreach (var s in statuses)
            {
                if (!s.IsFinal)
                {
                    args.Entity.StatusId = s.Id;
                    break;
                }
            }
            
            return args.Entity;
        }

        public Issue BeforeDelete(IssueEventArgs args)
        {
            return args.Entity;
        }

        public IssueComment BeforeComment(IssueCommentEventArgs args)
        {
            return args.Entity;
        }

        public Issue BeforeStatusChange(IssueEventArgs args)
        {
            return args.Entity;
        }

        public Issue BeforeResolutionChange(IssueEventArgs args)
        {
            return args.Entity;
        }

        public Issue BeforeAssign(IssueEventArgs args)
        {
            return args.Entity;
        }

        public Issue BeforeClose(IssueEventArgs args)
        {
            return args.Entity;
        }

        public Issue BeforeWatcherAdd(IssueEventArgs args)
        {
            return args.Entity;
        }

        public IssueDto BeforeCreateFull(IssueDtoEventArgs args)
        {
            return args.Issue;
        }

        public IssueDto BeforeUpdateFull(IssueDtoEventArgs args)
        {
            return args.Issue;
        }

        public IssueDto BeforeIssueCopy(IssueDtoEventArgs args)
        {
            return args.Issue;
        }

        public IssueDto CopyIssueComplete(IssueDtoEventArgs args)
        {
            return args.Issue;
        }

        public string Description { get; set; }

        public string Name { get; set; }

        public string AppGuid { get; set; }
    }
}
