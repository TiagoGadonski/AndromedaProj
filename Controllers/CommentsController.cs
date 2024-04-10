using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IBlogRepository _repository;

        public CommentsController(IBlogRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByPostId(int postId)
        {
            var comments = await _repository.GetCommentsByPostIdAsync(postId);

            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddCommentAsync(comment);

            return CreatedAtAction(nameof(GetCommentsByPostId), new { postId = comment.PostId }, comment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _repository.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            await _repository.DeleteCommentAsync(id);

            return NoContent();
        }
    }

}
