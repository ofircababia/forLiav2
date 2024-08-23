using Azure.Core;
using Castle.Components.DictionaryAdapter.Xml;
using iFlight.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Web_Api.Classes;
using Web_Api.DTO;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ArticlesController : ControllerBase
    {

        IFlightContext db = new IFlightContext();


        // צפייה בכתבות סעיף 11.1
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("getallArticles")]
        public ActionResult<Article> GetAllArticles()
        {
            try
            {
                List<Article> articles = db.Articles.ToList();
                return Ok(articles);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("getallArticleSubjects")]
        public ActionResult<string> GetArticleSubjects()
        {
            try
            {
                List<string> subs = db.ArticleSubjects.Select(x=> x.SubjectName).ToList();
                return Ok(subs);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        // צפייה בכתבות סעיף 11.2
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("addarticle")]
        public IActionResult AddArticle([FromBody] Article article)
        {
            try
            {
                if (article == null)
                {
                    return BadRequest(article);
                }
                else if (article.ArticleNumber!=0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                db.Articles.Add(article);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // יצירת נושאים לכתבות סעיף 12
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("addarticlesubject")]
        public IActionResult AddArticleSubject(string subjectname)
        {
            try
            {
                if (subjectname==null)
                {
                    return BadRequest(subjectname);
                }
                ArticleSubject sbj = new ArticleSubject();
                sbj.SubjectName = subjectname;
                db.ArticleSubjects.Add(sbj);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }  
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("deletearticlesubject")]
        public IActionResult DeleteArticleSubject(string subjectName)
        {
            try
            {
                if (subjectName == null)
                {
                    return BadRequest();
                }
                ArticleSubject AS = db.ArticleSubjects
                    .Where(x => x.SubjectName == subjectName).First();
                if (AS == null)
                {
                    return NotFound($"There is no subject by the name {subjectName}");
                }
                db.Remove(AS);
                db.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
