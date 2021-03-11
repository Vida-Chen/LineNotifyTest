# LineNotifyTest
範例簡述：取得中央氣象局提供的氣象資料，透過LineNotify傳送訊息至所屬Line群組，這次的web api專案也嘗試加上使用swagger。

> 背景：Line的普及性很高，甚至被用於很多工作場合，
> 我曾經待過一間公司，所有人每天不一定會收email，但無時無刻都一定會查看Line訊息，
> 所以曾經導致一支interface程式error了3天，都未有人查覺，
> 因緣際會下，想到了或許可試試LineNotify，由於Line的被使用率與工作密合度極高，且藉用其主動與即時的特性，
> 我們嘗試將LineNotify應用在了 - 當重要的interface程式出錯時，除了寄送hilight mail，也同時發送line訊息，增加了在第一時間警覺的效果。

前置準備：
- 登入中央氣象局開放平台，並申請取得API授權碼
- 登入LineNotify，申請開發人員用的發行存取權杖(一個token僅能對應一個line group或是個人)

後記：
可嘗試與其他服務連動試試
- GitHub
- IFTTT: 可以將不同的網路服務或應用程式整合至同一平台
- Mackerel: 一個為程式開發人員設計的伺服器監視服務

執行結果：
- API list
![](https://github.com/Vida-Chen/LineNotifyTest/blob/master/Screenshot/API_List.png?raw=true)

- API 1 test
![](https://github.com/Vida-Chen/LineNotifyTest/blob/master/Screenshot/API1.png?raw=true)

- API 1 test result
![](https://github.com/Vida-Chen/LineNotifyTest/blob/master/Screenshot/API1_Result.png?raw=true)

- API 2 test
![](https://github.com/Vida-Chen/LineNotifyTest/blob/master/Screenshot/API2.png?raw=true)

- API 2 test result
![](https://github.com/Vida-Chen/LineNotifyTest/blob/master/Screenshot/API2_Result.png?raw=true)

