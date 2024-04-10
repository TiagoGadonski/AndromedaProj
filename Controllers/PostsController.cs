using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IBlogRepository _repository;

        public PostsController(IBlogRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            return await _repository.GetPostsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            var post = await _repository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            await _repository.AddPostAsync(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            await _repository.UpdatePostAsync(post);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeletePostAsync(id);
            return NoContent();
        }
    }

}
