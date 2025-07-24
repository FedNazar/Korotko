/*
 * Korotko
 * Website
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

import { LinkException } from "../exceptions/link-exception.js";
import * as URLs from "../shared/urls.js";

export class LinkService {
    async getUrlByShortId(shortId) {
        const response = await fetch(URLs.GET_URL_ENDPOINT + shortId, {
            method: "GET",
        });

        if (!response.ok) {
            throw new LinkException(await(await response.json())["title"],
                response.status);
        }

        return response.text();
    }


    async createShortLink(url) {
        const response = await fetch(URLs.CREATE_LINK_ENDPOINT, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: `"${url}"`
        });

        if (!response.ok) {
            throw new LinkException(await(await response.json())["title"],
                response.status);
        }

        return URLs.WEBSITE_URL_WITH_PROTOCOL + await response.text();
    }
}