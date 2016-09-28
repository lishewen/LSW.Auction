FROM microsoft/aspnetcore:1.0.1
ENTRYPOINT ["dotnet", "拍卖系统.dll"]
ARG source=.
WORKDIR /app
EXPOSE 80
COPY $source .
