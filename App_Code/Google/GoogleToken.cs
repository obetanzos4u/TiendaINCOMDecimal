using Google.Apis.Auth;
using Google.Apis.Oauth2.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de GoogleTokenVerifier
/// </summary>
public class GoogleTokenVerifier {
    public GoogleTokenVerifier() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    private string _CLIENT_ID = "735817781293-gj8bqpplf0jt9au330hcejcp06js20di";
    public async void Validar(string AccessToken, string userid) {

        AccessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ijc3MjA5MTA0Y2NkODkwYTVhZWRkNjczM2UwMjUyZjU0ZTg4MmYxM2MiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwiYXpwIjoiNzM1ODE3NzgxMjkzLWdqOGJxcHBsZjBqdDlhdTMzMGhjZWpjcDA2anMyMGRpLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiYXVkIjoiNzM1ODE3NzgxMjkzLWdqOGJxcHBsZjBqdDlhdTMzMGhjZWpjcDA2anMyMGRpLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwic3ViIjoiMTE3OTM0NTUzMDk3MTg3NTYyNzk3IiwiaGQiOiJpbmNvbS5teCIsImVtYWlsIjoiY21pcmFuZGFAaW5jb20ubXgiLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiYXRfaGFzaCI6IllWdVItbUpqYzJjRlF1OEZaZnl3OEEiLCJuYW1lIjoiQ2FybG9zIE1pcmFuZGEiLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDMuZ29vZ2xldXNlcmNvbnRlbnQuY29tL2EtL0FPaDE0R2pDUTdKd0lGd2lpb3JuTzlfNkVuNVFmekVNNk9abWlfYjNSbEJSQWc9czk2LWMiLCJnaXZlbl9uYW1lIjoiQ2FybG9zIiwiZmFtaWx5X25hbWUiOiJNaXJhbmRhIiwibG9jYWxlIjoiZXMtNDE5IiwiaWF0IjoxNjIwMjU0MDMzLCJleHAiOjE2MjAyNTc2MzMsImp0aSI6IjUyOGE0N2UwMGJlMTEwMDFiNGJjYmMwNmE3Nzk3MGFhM2EzYjNjOTAifQ.QANJqM47wBqboEjKl4aNeb3deQBRB05TAElP19C1VC_xogXg3Wq65kSvr4gRNFvylt1ppdxqic_ChPkA6ObK9fUjDPvkAvbA7SadjIJimxj8QukFB_XypeTUQgPrrrmt6myPrq8gnHiuU5dY2AALApkVdG-AUuhyeD4GxxywpD3VRSq1cjTTQuHuYTThdQNwa4zZE-LWQrvTvBDNb4W1o9OCcn8roAeIM43T5Nv_YTrFdLOglaU3r9hCYl_tDIOj7gG_J7VnHFRfTX4h3XqQoicDX1LU_WDBhppL7I43qiOitmYwLrWvaQ2lS8bCAxPRHg6ItHKQ-_mmkgiYSQhTgA";

        var validPayload = await GoogleJsonWebSignature.ValidateAsync(AccessToken);
        string result = "";

    }
}