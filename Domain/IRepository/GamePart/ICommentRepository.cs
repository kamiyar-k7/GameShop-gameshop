using Domain.entities.Comments;

namespace Domain.IRepository.GamePart;

public interface ICommentRepository
{

    Task<List<Comments>> GetCommentsOfGame(int gameid);
    Task AddComment(Comments comments);
     Task<List<Comments>> AllComents();
    Task DeleteComment(int id);
}
