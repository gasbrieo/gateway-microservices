from fastapi import FastAPI, Request
from fastapi.responses import JSONResponse

app = FastAPI(title="ReportService")


@app.get("/api/v1/ping")
async def ping(request: Request):
    return JSONResponse(
        {
            "service": "ReportService",
            "message": "pong",
            "user_id": request.headers.get("x-user-id", "anonymous"),
            "role": request.headers.get("x-user-role", "none"),
            "correlation_id": request.headers.get("x-correlation-id", "none"),
        }
    )
