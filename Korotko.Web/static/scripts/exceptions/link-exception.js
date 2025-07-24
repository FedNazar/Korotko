/*
 * Korotko
 * Website
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

export class LinkException extends Error {
    constructor(message, statusCode) {
        super(message);
        this.statusCode = statusCode;
    }
}