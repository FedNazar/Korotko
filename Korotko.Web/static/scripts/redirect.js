/*
 * Korotko
 * Website
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

import * as URLs from "./shared/urls.js";
import { LinkService } from "./services/link-service.js";
import {LinkException} from "./exceptions/link-exception.js";

const shortId = (new URLSearchParams(window.location.search)).get("id");
if (!shortId) window.location = URLs.WEBSITE_URL_WITH_PROTOCOL;

let linkService = new LinkService();

try
{
    window.location = await linkService.getUrlByShortId(shortId);
}
catch (error)
{
    if (error instanceof LinkException) {
        if (error.statusCode === 404) window.location = URLs.LINK_NOT_FOUND_URL;
        else window.location = URLs.WEBSITE_URL_WITH_PROTOCOL;
    }
    else window.location = URLs.WEBSITE_URL_WITH_PROTOCOL;
}