/*
 * Korotko
 * Website
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

import * as URLs from "./shared/urls.js"
import { LinkService } from "./services/link-service.js";

const GENERATE_FORM = document.getElementById("generate-form");
const INSPECT_FORM = document.getElementById("inspect-form");

const LONG_URL_INPUT = document.getElementById("longUrl");
const SHORT_URL_OUTPUT = document.getElementById("shortUrl");

const SHORT_URL_INSPECT_INPUT = document.getElementById("shortUrl-inspect");
const LONG_URL_INSPECT_OUTPUT = document.getElementById("longUrl-inspect");

const BASE_URL_FOR_REGEX = RegExp.escape(URLs.WEBSITE_URL);
const BASE_URL_REGEX = new RegExp(`^(https:\/\/${BASE_URL_FOR_REGEX}|` +
    `http:\/\/${BASE_URL_FOR_REGEX}|${BASE_URL_FOR_REGEX})`);

let linkService = new LinkService();

GENERATE_FORM.addEventListener("submit", async event => {
    event.preventDefault();

    let url = LONG_URL_INPUT.value.trim();

    if (url.length === 0) {
        alert("Please enter a URL");
        return;
    }

    try
    {
        SHORT_URL_OUTPUT.value = await linkService.createShortLink(url);
    }
    catch (error)
    {
        alert(error.message);
    }
});

INSPECT_FORM.addEventListener("submit", async e => {
    e.preventDefault();

    let shortUrl = SHORT_URL_INSPECT_INPUT.value.trim();

    if (!shortUrl.match(BASE_URL_REGEX)) {
        alert("Please enter a valid URL");
        return;
    }

    let shortId = shortUrl.replace(BASE_URL_REGEX, "");

    if (shortId.length === 0) {
        alert("Please enter a valid URL");
        return;
    }

    try
    {
        LONG_URL_INSPECT_OUTPUT.value = await linkService.getUrlByShortId(shortId);
    }
    catch (error)
    {
        alert(error.message);
    }
});