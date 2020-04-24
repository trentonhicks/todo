using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories.EditSubItem
{
    public class EditSubItemUserStory : IRequestHandler<EditSubItem>
    {
        private ISubItemRepository _subItemRepository;
        public EditSubItemUserStory(ISubItemRepository subItemRepository)
        {
            _subItemRepository = subItemRepository;
        }
        public async Task<Unit> Handle(EditSubItem request, CancellationToken cancellationToken)
        {
            var subItem = await _subItemRepository.FindByIdAsync(request.SubItemId);

            subItem.Name = request.Name;

            await _subItemRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
