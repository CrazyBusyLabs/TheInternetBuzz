<?xml version="1.0" encoding="utf-8"?>
<%@ Page Language="C#" ContentType="text/xml" Inherits="TheInternetBuzz.XML.Sitemap" %>
<urlset xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd" xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
<url>
<loc>http://www.theinternetbuzz.com</loc>
<changefreq>hourly</changefreq>
<priority>1.0</priority>
</url>
<% 
    DisplayTrends();
    DisplayCategories(); 
%>
</urlset>