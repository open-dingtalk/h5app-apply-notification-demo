using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Top.Api.Util;

namespace SendInformationDemo.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private static readonly string agentId = ConfigurationManager.AppSettings["agentId"]; // 从配置文件中获取应用Id

        #region 从配置文件中获取配置相关
        public string getCorpId()
        {
            return ConfigurationManager.AppSettings["corpId"].ToString();
        }
        #endregion

        #region 通过免登授权码获取用户UserId
        public string getUserByCode(string code)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/v2/user/getuserinfo");
            OapiV2UserGetuserinfoRequest req = new OapiV2UserGetuserinfoRequest();
            req.Code = code;
            OapiV2UserGetuserinfoResponse rsp = client.Execute(req, AccessToken.AccessToken.GetAccessToken());
            return JsonConvert.SerializeObject(rsp.Result);
        }
        #endregion

        #region 发送文本消息
        public string SendText(string userId)
        {
            string content = "您好";
            var result = SendInformation(Convert.ToInt64(agentId), userId, content, AccessToken.AccessToken.GetAccessToken());
            return result.Errcode == 0 ? "1" : "-1";
        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="agentId">应用Id</param>
        /// <param name="users">用户UserId</param>
        /// <param name="content">消息内容</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public static OapiMessageCorpconversationAsyncsendV2Response SendInformation(long agentId, string users, string content, string token)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");
            OapiMessageCorpconversationAsyncsendV2Request request = new OapiMessageCorpconversationAsyncsendV2Request();
            request.AgentId = agentId;
            request.UseridList = users;
            request.ToAllUser = false;
            request.Msg_ = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain()
            {
                Msgtype = "text",
                Text = new OapiMessageCorpconversationAsyncsendV2Request.TextDomain()
                {
                    Content = content
                }

            };
            OapiMessageCorpconversationAsyncsendV2Response response = client.Execute(request, token);
            return response;
        }
        #endregion

        #region 发送图片消息
        public string SendImage(string userId)
        {
            // 从本地获取图片
            string imageUrl = AppDomain.CurrentDomain.BaseDirectory + "/Image/钉钉开放平台.png";
            FileInfo fInfo = new FileInfo(imageUrl);

            // 将图片上传至钉盘
            var result = SingleUpload(fInfo, AccessToken.AccessToken.GetAccessToken());
            if (result.Errcode == 0)
            {
                // 发送图片消息
                string content = result.MediaId;
                var response = SendImage(Convert.ToInt64(agentId), userId, content, AccessToken.AccessToken.GetAccessToken());
                return response.Errcode == 0 ? "1" : "-1";
            }
            return "-1";
        }

        /// <summary>
        /// 上传媒体文件到钉盘
        /// </summary>
        /// <param name="media">媒体文件</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public static OapiMediaUploadResponse SingleUpload(FileInfo media, string token)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/media/upload?type=image");
            OapiMediaUploadRequest req = new OapiMediaUploadRequest();
            FileItem item = new FileItem(media);
            req.Media = item;
            OapiMediaUploadResponse response = client.Execute(req, token);
            return response;
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="agentId">应用Id</param>
        /// <param name="users">用户UserId</param>
        /// <param name="content">钉盘中的媒体文件Id</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public static OapiMessageCorpconversationAsyncsendV2Response SendImage(long agentId, string users, string content, string token)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");
            OapiMessageCorpconversationAsyncsendV2Request request = new OapiMessageCorpconversationAsyncsendV2Request();
            request.AgentId = agentId;
            request.UseridList = users;
            request.ToAllUser = false;
            request.Msg_ = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain()
            {
                Msgtype = "image",
                Image = new OapiMessageCorpconversationAsyncsendV2Request.ImageDomain()
                {
                    MediaId = content
                }

            };
            OapiMessageCorpconversationAsyncsendV2Response response = client.Execute(request, token);
            return response;
        }
        #endregion

        #region 发送Markdown消息
        public string SendMarkdown(string userId)
        {
            string title = "发送markdown消息";
            string content = "# 这是支持markdown的文本   \n   ## 标题2    \n   * 列表1   \n";
            var result = SendInformation(Convert.ToInt64(agentId), userId, title, content, AccessToken.AccessToken.GetAccessToken());
            return result.Errcode == 0 ? "1" : "-1";
        }

        /// <summary>
        /// 发送Markdown消息
        /// </summary>
        /// <param name="agentId">应用Id</param>
        /// <param name="users">用户UserId</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public static OapiMessageCorpconversationAsyncsendV2Response SendInformation(long agentId, string users, string title, string content, string token)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");
            OapiMessageCorpconversationAsyncsendV2Request request = new OapiMessageCorpconversationAsyncsendV2Request();
            request.AgentId = agentId;
            request.UseridList = users;
            request.ToAllUser = false;
            request.Msg_ = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain()
            {
                Msgtype = "markdown",

                Markdown = new OapiMessageCorpconversationAsyncsendV2Request.MarkdownDomain()
                {
                    Title = title,
                    Text = content
                }
            };
            OapiMessageCorpconversationAsyncsendV2Response response = client.Execute(request, token);
            return response;
        }
        #endregion

        #region 发送卡片消息
        public string SendUrl(string userId)
        {
            string title = "卡片消息";
            string singleTitle = "查看详情";
            string markdown = "钉钉开放平台";
            string singleUrl = "https://open.dingtalk.com";
            var result = SendCard(Convert.ToInt64(agentId), userId, title, markdown, singleTitle, singleUrl, AccessToken.AccessToken.GetAccessToken());
            if (result.Errcode == 0)
            {
                return "1";
            }
            else
            {
                return "-1";
            }
        }

        /// <summary>
        /// 发送卡片消息
        /// </summary>
        /// <param name="agentId">应用Id</param>
        /// <param name="users">用户Id</param>
        /// <param name="title">标题</param>
        /// <param name="markdown">内容</param>
        /// <param name="singleTitle">按钮标题</param>
        /// <param name="singleUrl">按钮链接</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public static OapiMessageCorpconversationAsyncsendV2Response SendCard(long agentId, string users, string title, string markdown, string singleTitle, string singleUrl, string token)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");
            OapiMessageCorpconversationAsyncsendV2Request request = new OapiMessageCorpconversationAsyncsendV2Request();
            request.AgentId = agentId;
            request.UseridList = users;
            request.ToAllUser = false;
            request.Msg_ = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain()
            {
                Msgtype = "action_card",
                ActionCard = new OapiMessageCorpconversationAsyncsendV2Request.ActionCardDomain()
                {
                    Title = title,
                    Markdown = markdown,
                    SingleTitle = singleTitle,
                    SingleUrl = singleUrl
                }

            };
            OapiMessageCorpconversationAsyncsendV2Response response = client.Execute(request, token);
            return response;
        }
        #endregion
    }
}