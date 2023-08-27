using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common;
using WpfDemo.Event;

namespace WpfDemo.Extensions
{
    public static class DialogExtentions
    {
        /// <summary>
        /// 询问窗口
        /// </summary>
        /// <param name="dialogHost">会话主</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="dialogName">会话主没名称</param>
        /// <returns></returns>

        public async static Task<IDialogResult> Question(this IDialogHostService dialogHost,string title,string content,string dialogName="Root")
        {
            DialogParameters parame = new DialogParameters();
            parame.Add("Title", title);
            parame.Add("Content", content);
            parame.Add("DialogName", dialogName);
            var result = await dialogHost.ShowDialog("MsgView",parame,dialogName);
            return result;
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="model"></param>
        public static void UpdateLoading(this IEventAggregator eventAggregator,UpDateModel model)
        {
            eventAggregator.GetEvent<UpDateLodingEvent>().Publish(model);
        }

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="model"></param>
        public static void Register(this IEventAggregator eventAggregator,Action<UpDateModel> action)
        {
            eventAggregator.GetEvent<UpDateLodingEvent>().Subscribe(action);
        }

        /// <summary>
        /// 注册提示消息事件
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="action"></param>
        public static void RegisterMessge(this IEventAggregator eventAggregator, Action<MessageModel> action,string filterName="Main")
        {
            eventAggregator.GetEvent<MessageEvent>().Subscribe(action, ThreadOption.PublisherThread, true, (m) =>
            {
                return m.Filter.Equals(filterName);
            });
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="message"></param>
        public static void SendMessage(this IEventAggregator eventAggregator,string message, string filterName = "Main")
        {
            eventAggregator.GetEvent<MessageEvent>().Publish(new MessageModel()
            {
                Filter= filterName,
                Message=message
            });
        }
    }
}
