/*
 * Korotko
 * Website
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

import { SERVER_URL } from '../shared/server-url.js'

export const CREATE_LINK_ENDPOINT = SERVER_URL + "/api/links";
export const GET_URL_ENDPOINT = SERVER_URL + "/api/links/";

export const WEBSITE_URL = window.location.host + '/';
export const WEBSITE_URL_WITH_PROTOCOL = window.location.origin + '/';

export const LINK_NOT_FOUND_URL = WEBSITE_URL_WITH_PROTOCOL + "404.html";