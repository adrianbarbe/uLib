START TRANSACTION;

ALTER TABLE "Books" ALTER COLUMN "PreviewImageUrl" DROP NOT NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230116121359_BookPreviewImageNullable', '6.0.10');

COMMIT;

